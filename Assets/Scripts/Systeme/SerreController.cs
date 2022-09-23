using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerreController : MonoBehaviour
{
    private Batiment myBatiment;
    private PlaneteDetail myDetail;
    private TimeClass productionTimer;

    // Start is called before the first frame update
    void Start()
    {
        myBatiment = gameObject.GetComponent<BaseBatimentController>().myBatiment;
        myDetail = MainSysteme.Instance.allPlanetes[myBatiment.PlaneteId].GetComponent<BasePlaneteController>().myDetail;

        if (myBatiment.Niveau > 0) {
            productionTimer = this.gameObject.AddComponent<TimeClass>();
            productionTimer.nextTime = Constants.SerreBaseProduction;
            StartCoroutine(RunSerreProduction());
        }
    }
        
    IEnumerator RunSerreProduction()
    {
        productionTimer.initTimer(new DateTime(myBatiment.ProductionStart));
        if (productionTimer.nbTime > 0)
        {
            RealmController.Instance._realm.Write(() => {
                myDetail.Vivre += productionTimer.nbTime;
                myBatiment.ProductionStart = DateTime.UtcNow.AddMinutes(-productionTimer.currentTimeInMin).Ticks;
            });
        }

        yield return StartCoroutine(productionTimer.WaitTillNextTime());

        RealmController.Instance._realm.Write(() => {
            myDetail.Vivre += 1;
            myBatiment.ProductionStart = DateTime.UtcNow.Ticks;
        });

        StartCoroutine(RunSerreProduction());
    }

}
