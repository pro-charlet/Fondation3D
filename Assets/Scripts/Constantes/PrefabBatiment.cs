using System;
using System.Collections.Generic;

public static class PrefabBatiment
{
    public const string Logement = "LogementPrefab";
    public const string Usine = "UsinePrefab";

    public static Dictionary<Constants.Ressources, int[]> CostLogement = new Dictionary<Constants.Ressources, int[]>
    {
        {Constants.Ressources.Fer, new int[] {100, 200, 400, 800}},
        {Constants.Ressources.Pierre, new int[] {0, 0, 0, 0}},
        {Constants.Ressources.Cuivre, new int[] {0, 0, 0, 0}}
    };
}
