using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeDetecction
{
    Range,Melee
}

public class EnemigoInteraction : MonoBehaviour
{
    [SerializeField] private GameObject selectionFX;
    [SerializeField] private GameObject selectionMeleeFX;

    public void ShowEnemySelected(bool state,TypeDetecction type)
    {
        if(type == TypeDetecction.Range) 
        {
        selectionFX.SetActive(state);
        }
        else
        {
            selectionMeleeFX.SetActive(state);
        }
    }

    public void DisabledSpritesSelection()
    {
        selectionFX?.SetActive(false);
        selectionMeleeFX?.SetActive(false);
    }
}
