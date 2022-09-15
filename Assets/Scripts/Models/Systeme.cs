using Realms;
using MongoDB.Bson;
using Realms.Sync;

public class Systeme : RealmObject 
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    public string Name;

}
