using Realms;
using MongoDB.Bson;
using Realms.Sync;

public class Flotte : RealmObject
{
    [PrimaryKey][MapTo("_id")] public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    [Required][MapTo("Systeme_id")] public string SystemeId { get; set; }
    [Required] public string Prefab { get; set; } // Modèle de la flotte
    //public long MoveStart { get; set; }
    [MapTo("Planete_id")] public string PlaneteId { get; set; }
    //[MapTo("MoveTo_id")] public string MoveToId { get; set; }

    public static Flotte NewBaseFlotte(string systemeId, string planeteId)
    {
        Flotte newObj = new Flotte();
        newObj.Id = ObjectId.GenerateNewId();
        newObj.Prefab = Constants.FlottePrefab;
        newObj.SystemeId = systemeId;
        newObj.PlaneteId = planeteId;
        return newObj;
    }
}
