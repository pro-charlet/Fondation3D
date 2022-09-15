using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFlotteController : MonoBehaviour
{
    private GameObject origineObject;
    public GameObject OrigineObject {
        get { return origineObject; }
        set { origineObject = value; MoveToOrbit(); }
    }

    public Flotte myFlotte;
    private GameObject myCanvas;

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = GameObject.Find("Canvas");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveToOrbit()
    {
        gameObject.transform.SetPositionAndRotation(origineObject.transform.position + Vector3.forward * origineObject.transform.localScale.z, Quaternion.identity);
    }
}
