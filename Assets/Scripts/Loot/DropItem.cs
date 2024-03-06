using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItem 
{
    [Header("Info")]
    public string Name;
    public InventarioItem Item;
    public int Amount;

    [Header("Drop")]
   [Range(0,100)] public float PorcentageDrop;

    public bool ItemPickedUP { get; set; }
}
