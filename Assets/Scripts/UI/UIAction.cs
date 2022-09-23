using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAction : MonoBehaviour
{
    public static UIAction Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void BuildFlotte(PlaneteDetail myDetail)
    {
        Flotte newFlotte = Flotte.NewBaseFlotte(myDetail.SystemeId, myDetail.PlaneteId);

        RealmController.Instance._realm.Write(() => {
            myDetail.Fer -= 10;

            RealmController.Instance._realm.Add(newFlotte);
        });

        MainSysteme.Instance.InstantiateFlotte(newFlotte);

    }

    public void UpgradeBatiment(Batiment myBatiment, PlaneteDetail myDetail, Constants.Batiments myType)
    {
        /*
        if (myBatiment == null)
        {
            myBatiment = Batiment.NewBaseBatiment(myDetail.SystemeId, myDetail.PlaneteId, myType);
            RealmController.Instance._realm.Write(() => {
                RealmController.Instance._realm.Add(myBatiment);
            });
        }
        */

        RealmController.Instance._realm.Write(() => {
            myBatiment.Niveau += 1;
            myBatiment.ProductionStart = DateTime.UtcNow.Ticks;

            myDetail.Fer -= 100;
        });
    }

}
