using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSysteme : MonoBehaviour
{
    public static MainSysteme Instance { get; private set; }
    public GameObject rockPlanetePrefab;
    public GameObject earthPlanetePrefab;
    public GameObject gazPlanetePrefab;
    public GameObject flottePrefab;
    public GameObject logementPrefab;
    public GameObject usinePrefab;
    public GameObject serrePrefab;

    private Dictionary<string, GameObject> allPrefabs;

    public Dictionary<string, GameObject> allPlanetes { get; private set; }

    private async void Start()
    {
        Instance = this;

        await RealmController.Instance.Login(Parameters.UserAdmin, Parameters.PwdAdmin);
        string demoId = "62d96d3dda632cfd4dfdc94c";

        allPlanetes = new Dictionary<string, GameObject>();

        allPrefabs = new Dictionary<string, GameObject>();
        allPrefabs.Add(PrefabPlanete.Rock, rockPlanetePrefab);
        allPrefabs.Add(PrefabPlanete.Earth, earthPlanetePrefab);
        allPrefabs.Add(PrefabPlanete.Gaz, gazPlanetePrefab);
        allPrefabs.Add(Constants.FlottePrefab, flottePrefab);
        allPrefabs.Add(PrefabBatiment.Logement, logementPrefab);
        allPrefabs.Add(PrefabBatiment.Usine, usinePrefab);
        allPrefabs.Add(PrefabBatiment.Serre, serrePrefab);

        LoadSysteme(demoId);
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void LoadSysteme(string systemeId)
    {
        IEnumerable<Planete> planetes = RealmController.Instance.LoadPlanetes(systemeId);
        if (planetes != null)
        {
            foreach (Planete p in planetes)
            {
                GameObject obj = Instantiate(allPrefabs[p.Prefab], new Vector3(p.PositionX, p.PositionY, p.PositionZ), Quaternion.identity);
                obj.transform.localScale = p.Size * Vector3.one;
                obj.GetComponent<BasePlaneteController>().OID = p.Id.ToString();
                obj.GetComponent<BasePlaneteController>().myPlanete = p;

                allPlanetes.Add(p.Id.ToString(), obj);
            }

            IEnumerable<PlaneteDetail> planeteDetails = RealmController.Instance.LoadPlanetesDetails(systemeId);
            foreach (PlaneteDetail d in planeteDetails)
            {
                allPlanetes[d.PlaneteId].GetComponent<BasePlaneteController>().myDetail = d;
            }   

            IEnumerable<Batiment> batiments = RealmController.Instance.LoadBatiments(systemeId);
            foreach (Batiment b in batiments)
            {
                GameObject obj = InstantiateBatiment(b, allPlanetes[b.PlaneteId].transform);
                allPlanetes[b.PlaneteId].GetComponent<BasePlaneteController>().AddBatiment(obj);
            }   

            IEnumerable<Flotte> flottes = RealmController.Instance.LoadFlottes(systemeId);
            foreach (Flotte f in flottes)
            {
                GameObject obj = InstantiateFlotte(f);

                /*MoveController moveCtrl = obj.AddComponent<MoveController>();
                moveCtrl.lastTime = DateTime.UtcNow.AddMinutes(-9);
                moveCtrl.moveToObject = allPlanetes["62dabc2882d9eb3b089e34fb"];
                */

            }
        } else
            Debug.Log("No Planetes !");

    }

    public GameObject InstantiateFlotte(Flotte flo)
    {
        GameObject obj = Instantiate(allPrefabs[flo.Prefab], new Vector3(0, 0, 0), Quaternion.identity);
        obj.GetComponent<BaseFlotteController>().myFlotte = flo;
        obj.GetComponent<BaseFlotteController>().OrigineObject = allPlanetes[flo.PlaneteId];

        return obj;
    }

    public GameObject InstantiateBatiment(Batiment bat, Transform planete)
    {
        GameObject obj = Instantiate(allPrefabs[bat.Prefab], new Vector3(0, 0, 0), Quaternion.identity, planete);
        obj.GetComponent<BaseBatimentController>().myBatiment = bat;

        return obj;
    }


}
