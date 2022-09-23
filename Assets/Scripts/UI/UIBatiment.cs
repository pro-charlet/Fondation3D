using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBatiment : MonoBehaviour
{
    public bool isContentChange;
    public GameObject selectedObject;
    private IUIBatimentContent selectedBatimentContent;

    public Constants.Batiments myType;
    [SerializeField] private TextMeshProUGUI textNiveau;
    [SerializeField] private Button btnUp;

    public interface IUIBatimentContent
    {
        Batiment GetBatiment();
        string GetNiveau();
        string GetTypeText();
        bool IsUpgradable();
    }


    private void Update()
    {
        if (isContentChange)
        {
            isContentChange = false;

            if (selectedObject != null)
            {
                selectedBatimentContent = selectedObject.GetComponentInParent<IUIBatimentContent>();
                textNiveau.text = selectedBatimentContent.GetTypeText() + " Niv " + selectedBatimentContent.GetNiveau();

                btnUp.interactable = selectedBatimentContent.IsUpgradable();

            }
        }
    }

    public void HandleUpButton()
    {
        GameObject obj = MainSysteme.Instance.allPlanetes[UISysteme.Instance.selectedPlaneteContent.GetOID()];
        UIAction.Instance.UpgradeBatiment(selectedBatimentContent.GetBatiment(), obj.GetComponent<BasePlaneteController>().myDetail, myType);

        isContentChange = true;
    }

}
