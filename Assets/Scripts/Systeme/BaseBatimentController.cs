using System;
using System.Collections.Generic;
using MongoDB.Bson;
using UnityEngine;

public class BaseBatimentController : MonoBehaviour,
    UIBatiment.IUIBatimentContent
{
    public Batiment myBatiment;
    public Constants.Batiments myType;
    public string myTypeText;
    public string myPrefabText;
    public int maxNiveau;

    public Batiment NewBatiment(string systemeId, string planeteId)
    {
        Batiment newObj = new Batiment();
        newObj.Id = ObjectId.GenerateNewId();
        newObj.SystemeId = systemeId;
        newObj.PlaneteId = planeteId;
        newObj.Niveau = 0;
        newObj.Prefab = myPrefabText;
        newObj.Type = myTypeText;

        return newObj;
    }

    public bool IsUpgradable()
    {
        return (IsNiveauUpgradable() && IsCostUpgradable());
    }

    private bool IsNiveauUpgradable()
    {
        return myBatiment.Niveau < maxNiveau;
    }

    private bool IsCostUpgradable()
    {
        bool canUp = true;
        foreach(Constants.Ressources res in (Constants.Ressources[]) Enum.GetValues(typeof(Constants.Ressources)))
        {
            canUp &= GetCostQuantity(res) <= UISysteme.Instance.selectedPlaneteContent.GetRessource(res);
        }

        return canUp;
    }

    public Batiment GetBatiment()
    {
        return myBatiment;
    }

    public string GetNiveau()
    {
        return myBatiment.Niveau.ToString();
    }

    public string GetTypeText()
    {
        return myTypeText;
    }

    private int GetCostQuantity(Constants.Ressources res)
    {
        if (PrefabBatiment.CostBatiment[myTypeText].ContainsKey(res))
            return PrefabBatiment.CostBatiment[myTypeText][res][myBatiment.Niveau];

        return 0;
    }

}
