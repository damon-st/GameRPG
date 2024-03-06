using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMarketPlace : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;
    [SerializeField] private TextMeshProUGUI cantidadForBuy;

    public ItemSale ItemLoad { get; private set; }

    private int amount;
    private int costInit;
    private int costActual;


    private void Update()
    {
        cantidadForBuy.text = amount.ToString();
        itemCost.text = costActual.ToString();
    }


    public void ConfigItemSale(ItemSale itemSale) 
    {
        ItemLoad=itemSale;
        itemIcon.sprite = itemSale.Item.Icon;
        itemName.text = itemSale.Item.Name;
        itemCost.text = itemSale.Cost.ToString();
        amount = 1;
        costInit = itemSale.Cost;
        costActual = itemSale.Cost;
    }

    public void BuyItem()
    {
        if(MonedasManager.Instance.CointsTotals>= costActual)
        {
            Inventario.Instance.AddItem(ItemLoad.Item, amount);
            MonedasManager.Instance.RemoveCoins(costActual);
            amount = 1;
            costActual = costInit;
        }
    }

    public void SumarItemForBuy()
    {
        int costOfBuy = costInit * (amount + 1);
        if(MonedasManager.Instance.CointsTotals>= costOfBuy)
        {
            amount++;
            costActual = costInit * amount;
        }
    }

    public void RestarItemForBuy()
    {
        if (amount == 1) return;
        amount--;
        costActual= costInit * amount;
    }
}
