using System;
using System.Collections.Generic;

public static class PrefabBatiment
{
    public const string Logement = "LogementPrefab";
    public const string Usine = "UsinePrefab";
    public const string Serre = "SerrePrefab";

    public static Dictionary<string, Dictionary<Constants.Ressources, int[]>> CostBatiment = new Dictionary<string, Dictionary<Constants.Ressources, int[]>>
    {
        {Batiment.Logement, new Dictionary<Constants.Ressources, int[]> {
            {Constants.Ressources.Fer, new int[] {100, 200, 400, 800}},
            {Constants.Ressources.Pierre, new int[] {0, 0, 0, 0}},
            {Constants.Ressources.Cuivre, new int[] {0, 0, 0, 0}}
            }
        },
        {Batiment.Usine, new Dictionary<Constants.Ressources, int[]> {
            {Constants.Ressources.Fer, new int[] {100, 200, 400, 800}},
            {Constants.Ressources.Pierre, new int[] {0, 0, 0, 0}},
            {Constants.Ressources.Cuivre, new int[] {0, 0, 0, 0}}
            }
        },
        {Batiment.Serre, new Dictionary<Constants.Ressources, int[]> {
            {Constants.Ressources.Fer, new int[] {100, 200, 400, 800}},
            {Constants.Ressources.Pierre, new int[] {0, 0, 0, 0}},
            {Constants.Ressources.Cuivre, new int[] {0, 0, 0, 0}}
            }
        }
    };

}
