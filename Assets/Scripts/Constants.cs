using System;
using System.Collections;
using System.Collections.Generic;

public static class Constants
{
    public const string FlottePrefab = "FlottePrefab";
    public const string BatimentPrefab = "BatimentPrefab";


    public static TimeSpan MineBaseExtraction = new TimeSpan(24,0,0);
    public static TimeSpan TimeTravelBetweenPlanete = new TimeSpan(0,10,0);

    public static TimeSpan SerreBaseProduction = new TimeSpan(0,5,0);

    public enum Ressources {Fer, Pierre, Cuivre, Vivre};
    public enum Batiments {None, Logement, Usine, Astroport, Serre}; //Type des batiments
}
