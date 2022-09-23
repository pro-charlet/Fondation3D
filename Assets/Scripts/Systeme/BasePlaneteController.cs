using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlaneteController : MonoBehaviour,
        UISysteme.IUIPlaneteContent
{
    public string OID;
    public Planete myPlanete;
    public PlaneteDetail myDetail;
    public Dictionary<Constants.Batiments, GameObject> myBatiments;
    public List<GameObject> orbitFlotte;
    public GameObject minierFlottePrefab;
    private MinierController myMinierCtrl;

    private float rotateSpeed;

    private void Start()
    {
        orbitFlotte = new List<GameObject>();

        if ((myPlanete != null) && (myDetail != null) && (myDetail.Minier > 0))
        {
            Vector3 minierPosition = new Vector3(myPlanete.PositionX, myPlanete.PositionY, myPlanete.PositionZ);
            minierPosition.x += myPlanete.Size + 1;

            GameObject myMinier = Instantiate(minierFlottePrefab, minierPosition, Quaternion.identity);
            myMinierCtrl = myMinier.GetComponent<MinierController>();
            myMinierCtrl.orbitPlanete = gameObject;
            orbitFlotte.Add(myMinier);

        } else {
        }
    }

    private void Update()
    {
        if (myPlanete != null)
        {
            rotateSpeed = 12f / myPlanete.Size;
            this.transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
        }
    }
    void OnMouseDown()
    {
        UISysteme.Instance.ChangeContent(gameObject);

    }

    public void AddBatiment(GameObject bat)
    {
        if (myBatiments == null)
            myBatiments = new Dictionary<Constants.Batiments, GameObject>();

        myBatiments.Add(bat.GetComponent<BaseBatimentController>().myType, bat);
    }

    public GameObject GetBatiment(Constants.Batiments type)
    {
        if ((myBatiments != null) && (myBatiments.ContainsKey(type)))
            return myBatiments[type];
    
        return null;
    }

    public string GetOID()
    {
        return OID;
    }

    public Material GetMaterial()
    {
        return gameObject.GetComponent<Renderer>().material;
    }

    public bool IsDirty()
    {
        return true;
    }
    public bool IsEarth()
    {
        return (myPlanete.Prefab == PrefabPlanete.Earth);
    }

    public long GetRessource(Constants.Ressources res)
    {
        if (res == Constants.Ressources.Fer)
            return myDetail.Fer;
        if (res == Constants.Ressources.Pierre)
            return myDetail.Pierre;
        if (res == Constants.Ressources.Vivre)
            return myDetail.Vivre;

        return 0;
    }

/*    public bool IsBatimentActive(Constants.Batiments bat)
    {
        bool isActive = false;
        if (myBatiments.ContainsKey(bat))
            isActive = (myBatiments[bat].Niveau > 0);

        return isActive;
    }
*/
    public string GetMinierNextExtractionTime()
    {
        if (myMinierCtrl != null)
            return myMinierCtrl.GetMinierNextExtractionTime();

        return "00:00";
    }

    public string GetMinierTotalExtractionTime()
    {
        if (myMinierCtrl != null)
            return myMinierCtrl.GetMinierTotalExtractionTime();

        return "00:00";
    }
}
