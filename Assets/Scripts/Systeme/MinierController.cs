using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MinierController : MonoBehaviour
{
    public GameObject orbitPlanete;
    private Planete myPlanete;
    private PlaneteDetail myDetail;

    public TimeSpan timerMinier; // Durée entre chaque extraction
    private TimeClass minierTimer;

    void Start()
    {
        DateTime dNow = DateTime.UtcNow;

        if (orbitPlanete != null) {
            myDetail = orbitPlanete.GetComponent<BasePlaneteController>().myDetail;

            if (myDetail.Minier > 0)
            {
                minierTimer = this.gameObject.AddComponent<TimeClass>();
                minierTimer.nextTime = Constants.MineBaseExtraction / myDetail.Minier;
                StartCoroutine(RunMinier());
            }
        }
    }
        
    IEnumerator RunMinier()
    {
        minierTimer.initTimer(new DateTime(myDetail.MinierStart));
        if (minierTimer.nbTime > 0)
        {
            RealmController.Instance._realm.Write(() => {
                myDetail.Fer += minierTimer.nbTime;
                myDetail.MinierStart = DateTime.UtcNow.AddMinutes(-minierTimer.currentTimeInMin).Ticks;
            });
        }

        yield return StartCoroutine(minierTimer.WaitTillNextTime());

        RealmController.Instance._realm.Write(() => {
            myDetail.Fer += 1;
            myDetail.MinierStart = DateTime.UtcNow.Ticks;
        });

        StartCoroutine(RunMinier());
    }

    public string GetMinierNextExtractionTime()
    {
        String sNextExtraction = "0:00";
        if ((minierTimer != null) && (minierTimer.currentTimeInMin > 0))
            sNextExtraction = string.Format("{0:0}:{1:00}", Math.Floor(minierTimer.currentTimeInMin/60), minierTimer.currentTimeInMin%60);

        return sNextExtraction;
    }

    public string GetMinierTotalExtractionTime()
    {
        String sTimeExtraction = "0:00";
        if (minierTimer != null)
            sTimeExtraction = string.Format("{0:0}:{1:00}", 24*minierTimer.nextTime.Days+minierTimer.nextTime.Hours, minierTimer.nextTime.Minutes);

        return sTimeExtraction;
    }


}
