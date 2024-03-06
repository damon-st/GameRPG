using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketPlaceManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private ItemMarketPlace itemMarketPlacePrefab;
    [SerializeField] private Transform panelContainer;

    [Header("Items")]
    [SerializeField] private ItemSale[] itemsAvalibles;


    private void Start()
    {
        LoadItemsInSale();
    }

    private void LoadItemsInSale()
    {
        for (int i = 0; i < itemsAvalibles.Length; i++)
        {
            ItemMarketPlace item = Instantiate(itemMarketPlacePrefab, panelContainer);
            item.ConfigItemSale(itemsAvalibles[i]);
        }
    }

}
