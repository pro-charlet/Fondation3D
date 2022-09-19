using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBatiment : MonoBehaviour
{
    public bool isContentChange;

    public Batiment myBatiment;
    public Constants.Batiments myType;
    [SerializeField] private TextMeshProUGUI textNiveau;
    [SerializeField] private Button btnUp;

    private void Update()
    {
        if (isContentChange)
        {
            isContentChange = false;

            btnUp.interactable = this.IsUpgradable();

            if (myBatiment != null)
            {
                textNiveau.text = "Niv " + myBatiment.Niveau.ToString();
            } else {
                textNiveau.text = "Niveau ";
            }
        }
    }

    public void HandleUpButton()
    {
        GameObject obj = MainSysteme.Instance.allPlanetes[UISysteme.Instance.selectedPlaneteContent.GetOID()];
        myBatiment = UIAction.Instance.UpgradeBatiment(myBatiment, obj.GetComponent<BasePlaneteController>().myDetail, myType);

        isContentChange = true;
    }

    private bool IsUpgradable()
    {
        bool canUp = true;

        if (myType == Constants.Batiments.Logement)
        foreach(Constants.Ressources res in PrefabBatiment.CostLogement.Keys)
        {
            if (myBatiment == null) {
                canUp &= (PrefabBatiment.CostLogement[res][0] <= UISysteme.Instance.selectedPlaneteContent.GetRessource(res));

            } else {
                canUp &= myBatiment.IsUpgradable(myType);
                canUp &= (PrefabBatiment.CostLogement[res][myBatiment.Niveau] <= UISysteme.Instance.selectedPlaneteContent.GetRessource(res));
            }
        }

        return canUp;
    }

}
