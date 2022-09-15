using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClass : MonoBehaviour
{
    private DateTime startTime; // DateHeure du dernier calcul
    public TimeSpan nextTime; // Durée entre chaque calcul
    public double currentTimeInMin; // Temps restant en minutes
    public long nbTime; // Nombre de calcul déjà dépassé

    public void initTimer(DateTime lastTime)
    {
        TimeSpan currentTime = DateTime.UtcNow - lastTime;

        nbTime = ((long)Math.Floor(currentTime.TotalSeconds / nextTime.TotalSeconds));
        currentTimeInMin = (nextTime.TotalSeconds - (currentTime.TotalSeconds % nextTime.TotalSeconds)) / 60;
    }

    public IEnumerator WaitTillNextTime()
    {
        yield return StartCoroutine(runNext());

    }

    IEnumerator runNext()
    {
        while (currentTimeInMin > 0)
        {   
            currentTimeInMin--;
            yield return new WaitForSeconds(60f);
            
        }
    }


}
