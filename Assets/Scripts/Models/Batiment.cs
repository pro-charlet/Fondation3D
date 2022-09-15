using Realms;
using MongoDB.Bson;
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

}
