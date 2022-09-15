using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISysteme : MonoBehaviour
{
    public static UISysteme Instance { get; private set; }
    public GameObject selectedObject;

    protected IUIPlaneteContent selectedPlaneteContent;
    protected bool isContentChange;

    [SerializeField] private TextMeshProUGUI textOID;
    [SerializeField] private TextMeshProUGUI textFer;
    [SerializeField] private TextMeshProUGUI textExtraction;

    [SerializeField] private GameObject objectUI;
    [SerializeField] private List<GameObject> panelBatiments;

    public interface IUIPlaneteContent
    {
        bool IsDirty();
        string GetOID();
        Material GetMaterial();
        string GetRessource(Constants.Ressources res);
        Batiment GetBatiment(Constants.Batiments bat);
        string GetMinierNextExtractionTime();
        string GetMinierTotalExtractionTime();
    }

    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        if (selectedPlaneteContent == null)
            return;

        if (isContentChange || selectedPlaneteContent.IsDirty())
        {
            isContentChange = false;

            textOID.text = "OID : " + selectedPlaneteContent.GetOID();
            
            objectUI.GetComponent<Renderer>().material = selectedPlaneteContent.GetMaterial();
            objectUI.SetActive(true);

            textFer.text = selectedPlaneteContent.GetRessource(Constants.Ressources.Fer);
            textExtraction.text = "Prochaine extraction dans : " + selectedPlaneteContent.GetMinierNextExtractionTime() + " / " + selectedPlaneteContent.GetMinierTotalExtractionTime();
        }
    }

    public void ChangeContent(GameObject content)
    {
        isContentChange = true;
        if (content != null)
        {
            selectedObject = content;
            selectedPlaneteContent = content.GetComponentInParent<IUIPlaneteContent>();
            foreach(GameObject obj in panelBatiments)
            {
                UIBatiment uiBat = obj.GetComponent<UIBatiment>();
                uiBat.myBatiment = selectedPlaneteContent.GetBatiment(uiBat.myType);
                uiBat.isContentChange = true;

                if (uiBat.myBatiment != null)
                Debug.Log(uiBat.myBatiment.Id);
            }
        }
    }

    public void HandleTestButton()
    {
        var oid = selectedPlaneteContent.GetOID();
        GameObject obj = MainSysteme.Instance.allPlanetes[oid];
        UIAction.Instance.BuildFlotte(obj.GetComponent<BasePlaneteController>().myDetail);
    }

}
