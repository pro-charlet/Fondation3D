using Realms;
using MongoDB.Bson;
using Realms.Sync;

public class Planete : RealmObject 
{
    [PrimaryKey][MapTo("_id")] public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    [Required][MapTo("Systeme_id")] public string SystemeId { get; set; }
    public string Name { get; set; }
    [Required] public string Prefab { get; set; } // Modèle de la planète
    public int Size { get; set; } // Taille de la planète
    
    // Position de la planète dans le système (X, Y, Z)
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }

    // Concentration de la ressource sur la planète
    public float Fer { get; set; } 
    public float Cuivre { get; set; }

}
