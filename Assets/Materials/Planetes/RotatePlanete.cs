using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlanete : MonoBehaviour
{
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angleY = this.transform.rotation.y;
        this.transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
