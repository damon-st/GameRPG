using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Receta 
{
    public string Name;
    [Header("1er Material")]
    public InventarioItem Item1;
    public int Item1AmountRequired;

    [Header("2er Material")]
    public InventarioItem Item2;
    public int Item2AmountRequired;

    [Header("Resultado")]
    public InventarioItem ItemResultado;
    public int ItemResultadoAmount;
}
