using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SolarSysteme : MonoBehaviour
{
    private readonly float g = 100f;
    public GameObject sun;
    private GameObject[] celestials;

    public void StartVelocity(GameObject[] otherCelestials)
    {
        celestials = celestials.Concat(otherCelestials).ToArray();
        initialVelocity(otherCelestials);  
    }

    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestials");
        initialVelocity(celestials);  
    }

    void FixedUpdate()
    {
        Gravity();     
    }

    private void Gravity()
    {
        foreach (GameObject a in celestials)
        foreach (GameObject b in celestials)
        {
            if (!a.Equals(b))
            {
                float m1 = a.GetComponent<Rigidbody>().mass;
                float m2 = b.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, b.transform.position);

                a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (g * (m1 * m2) / (r * r)));
            }                
            
        }
    }

    private void initialVelocity(GameObject[] listCelestials)
    {
        foreach (GameObject a in listCelestials)
        {
            if (!a.Equals(sun))
            {
                float m2 = sun.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(a.transform.position, sun.transform.position);
                a.transform.LookAt(sun.transform);

                a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((g * m2) / r);

            }
        }
    }

}
