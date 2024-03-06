using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecetaTarjeta : MonoBehaviour
{
    [SerializeField] private Image recetaIcon;
    [SerializeField] private TextMeshProUGUI recetaName;

    public Receta RecetaLoad { get; private set; }

    public void ConfigRecetaTarjeta(Receta receta)
    {
        RecetaLoad = receta;
        recetaIcon.sprite = receta.ItemResultado.Icon;
        recetaName.text = receta.ItemResultado.Name;
    }


    public void SelectReceta() 
    {
        UIManager.Instance.OpenClosePanelCraftingInfo(true);
        CraftingManager.Instance.MostarReceta(RecetaLoad);
    }
}
