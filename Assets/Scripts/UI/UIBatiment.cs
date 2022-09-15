using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBatiment : MonoBehaviour
{
    public bool isContentChange;

    public Batiment myBatiment;
    public Constants.Batiments myType;
    [SerializeField] private TextMeshProUGUI textNiveau;

    private void Update()
    {
        if (isContentChange)
        {
            isContentChange = false;

            if (myBatiment != null)
            {
                textNiveau.text = "Niv " + myBatiment.Niveau.ToString();
            } else {
                textNiveau.text = "Niveau ";
            }
        }
    }
}
