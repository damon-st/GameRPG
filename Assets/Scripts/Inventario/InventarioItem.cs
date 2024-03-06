using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypesItems
{
    Weapons,
    Potions,
    Scrolls,
    Ingredients,
    Treasures
}


public class InventarioItem : ScriptableObject
{

    [Header("Parametros")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea]public string Description;


    [Header("Inforamcion")]
    public TypesItems TypeItem;
    public bool IsConsumible;
    public bool IsAccumulation;
    public int AccumulationMax;

    [HideInInspector]public int Amount;


    public InventarioItem CopyItem()
    {
        InventarioItem newInstance = Instantiate(this);
        return newInstance;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual bool ToEquipItem()
    {
        return true;
    }

    public virtual bool RemoveItem()
    {
        return true;
    }

    public virtual string DescriptionItemCrafting()
    {
        return "";
    }
}
