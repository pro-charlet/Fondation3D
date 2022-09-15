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
        Flotte newFlotte = new Flotte();
        newFlotte.Id = ObjectId.GenerateNewId();
        newFlotte.Prefab = Constants.FlottePrefab;
        newFlotte.SystemeId = systemeId;
        newFlotte.PlaneteId = planeteId;
        return newFlotte;
    }
}
