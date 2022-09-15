using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public DateTime lastTime;
    public GameObject moveToObject;

    private GameObject origineObject;

    // Start is called before the first frame update
    void Start()
    {
        origineObject = gameObject.GetComponent<BaseFlotteController>().OrigineObject;
        StartCoroutine(moveNext());
    }

    IEnumerator moveNext()
    {
        float travelFraction = NextTravelProgressTo();
        while (travelFraction < 1)
        {   
            yield return new WaitForSeconds(1f);

            travelFraction = NextTravelProgressTo();
        }

        gameObject.GetComponent<BaseFlotteController>().OrigineObject = moveToObject;
        Destroy(this);
    
    }

    private float NextTravelProgressTo()
    {
        float travelFraction = Math.Min((float)((DateTime.UtcNow - lastTime) / Constants.TimeTravelBetweenPlanete), 1f);

        Vector3 v1 = origineObject.transform.position + Vector3.forward * origineObject.transform.localScale.z;
        Vector3 v2 = moveToObject.transform.position + Vector3.forward * origineObject.transform.localScale.z;
        transform.position = Vector3.Lerp(v1, v2, travelFraction);

        return travelFraction;
    }

}
