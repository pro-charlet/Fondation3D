using Realms;
using MongoDB.Bson;
using System.Collections.Generic;
using Realms.Sync;

public class Batiment : RealmObject
{
    [PrimaryKey][MapTo("_id")] public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    [Required][MapTo("Systeme_id")] public string SystemeId { get; set; }
    [Required][MapTo("Planete_id")] public string PlaneteId { get; set; }
    [Required] public string Prefab { get; set; } // Modèle du batiment
    [Required] public string Type { get; set; } // Type du batiment
    public int Niveau { get; set; } // Niveau du batiment


    public const string Logement = "Logement";
    public const string Usine = "Usine";
    public const string Astroport = "Astroport";

    public const int MaxNiveauLogement = 4;
    public const int MaxNiveauUsine = 4;
    public const int MaxNiveauAstroport = 4;


    public static Batiment NewBaseBatiment(string systemeId, string planeteId, Constants.Batiments type)
    {
        Batiment newObj = new Batiment();
        newObj.Id = ObjectId.GenerateNewId();
        newObj.SystemeId = systemeId;
        newObj.PlaneteId = planeteId;
        newObj.Niveau = 0;

        if (type == Constants.Batiments.Logement)
        {
            newObj.Prefab = PrefabBatiment.Logement;
            newObj.Type = Batiment.Logement;
        } else if (type == Constants.Batiments.Usine)
        {
            newObj.Prefab = PrefabBatiment.Usine;
            newObj.Type = Batiment.Usine;
        }

        return newObj;
    }

    public bool IsUpgradable(Constants.Batiments type)
    {
        bool canUp = false;
        if (type == Constants.Batiments.Logement)
            canUp = this.Niveau < MaxNiveauLogement;
        if (type == Constants.Batiments.Usine)
            canUp = this.Niveau < MaxNiveauUsine;
        if (type == Constants.Batiments.Astroport)
            canUp = this.Niveau < MaxNiveauAstroport;

        return canUp;
    }

}
