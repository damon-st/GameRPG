using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private InventarioItem inventoryItemRef;
    [SerializeField] private int amountForAdd;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventario.Instance.AddItem(inventoryItemRef, amountForAdd);
            Destroy(gameObject);
        }
    }
}
