using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContainer;

    [Header("Receta Info")]
    [SerializeField] private Image firstMaterialIcon;
    [SerializeField] private Image secondMaterialIcon;
    [SerializeField] private TextMeshProUGUI firstMaterialName;
    [SerializeField] private TextMeshProUGUI secondMaterialName;
    [SerializeField] private TextMeshProUGUI firstMaterialAmount;
    [SerializeField] private TextMeshProUGUI secondMaterialAmount;
    [SerializeField] private TextMeshProUGUI recetaMesagge;
    [SerializeField] private Button buttonCraftear;

    [Header("Item Resultado")]
    [SerializeField] private Image itemResultadoIcon;
    [SerializeField] private TextMeshProUGUI itemResultadoName;
    [SerializeField] private TextMeshProUGUI itemResultadoDescription;



    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;

    public Receta RecetaSelected { get; set; }

    private void Start()
    {
        LoadRecetas();
    }

    private void LoadRecetas()
    {
        for (int i = 0; i < recetas.Recetas.Length; i++)
        {
            RecetaTarjeta receta = Instantiate(recetaTarjetaPrefab, recetaContainer);
            receta.ConfigRecetaTarjeta(recetas.Recetas[i]);
        }
    }

    public void MostarReceta(Receta receta) 
    {
        RecetaSelected = receta;
        firstMaterialIcon.sprite = receta.Item1.Icon;
        secondMaterialIcon.sprite = receta.Item2.Icon;
        firstMaterialName.text = receta.Item1.Name;
        secondMaterialName.text = receta.Item2.Name;
        firstMaterialAmount.text = $"{Inventario.Instance.GetAmountOfItem(receta.Item1.ID)}/{receta.Item1AmountRequired}";
        secondMaterialAmount.text = $"{Inventario.Instance.GetAmountOfItem(receta.Item2.ID)}/{receta.Item2AmountRequired}";
        if (CanBeCrafting(receta))
        {
            recetaMesagge.text = "Receta Disponible";
            buttonCraftear.interactable = true;
        }
        else 
        {
            recetaMesagge.text = "Necesitas mas Materiales";
            buttonCraftear.interactable= false;
        }

        itemResultadoIcon.sprite = receta.ItemResultado.Icon;
        itemResultadoName.text = receta.ItemResultado.Name;
        itemResultadoDescription.text = receta.ItemResultado.DescriptionItemCrafting();
    }

    public bool CanBeCrafting(Receta receta)
    {
        return Inventario.Instance.GetAmountOfItem(receta.Item1.ID) >= receta.Item1AmountRequired
              && Inventario.Instance.GetAmountOfItem(receta.Item2.ID) >= receta.Item2AmountRequired;
    }


    public void Crafting()
    {
        for (int i = 0; i < RecetaSelected.Item1AmountRequired; i++)
        {
            Inventario.Instance.ConsumeItem(RecetaSelected.Item1.ID);
        }
        for (int i = 0; i < RecetaSelected.Item2AmountRequired; i++)
        {
            Inventario.Instance.ConsumeItem(RecetaSelected.Item2.ID);
        }
        Inventario.Instance.AddItem(RecetaSelected.ItemResultado,RecetaSelected.ItemResultadoAmount);
        MostarReceta(RecetaSelected);
    }

}
