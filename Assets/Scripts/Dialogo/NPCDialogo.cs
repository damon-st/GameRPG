using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InterationExtraNPC
{
    Quets,
    Marketplace,
    Crafting
}

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{

    [Header("Info")]
    public string Name;
    public Sprite Icon;
    public bool ConteintInteractionExtra;
    public InterationExtraNPC InterationExtra;

    [Header("Saludo")]
    [TextArea]public string Saludo;

    [Header("Chat")]
    public DialogoText[] Conversacion;

    [Header("Despedida")]
    [TextArea] public string Despedida;

}

[Serializable]
public class DialogoText
{
    [TextArea] public string Oracion;
}