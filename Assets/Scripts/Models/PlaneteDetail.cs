using Realms;
using MongoDB.Bson;
using Realms.Sync;

public class PlaneteDetail : RealmObject 
{
    [PrimaryKey][MapTo("_id")] public ObjectId  Id { get; set; }
    [Required][MapTo("Planete_id")] public string PlaneteId { get; set; }
    [Required][MapTo("Systeme_id")] public string SystemeId { get; set; }
    public long Minier { get; set; }
    public long MinierStart { get; set; }
    public long Pierre { get; set; }
    public long Fer { get; set; }
    public long Vivre { get; set; }

    public long CycleStart { get; set; }
}
