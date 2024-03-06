using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoLoot : MonoBehaviour
{
    [Header("Exp")]
    [SerializeField] private float expWin;


    [Header("Loot")]
    [SerializeField] private DropItem[] lootAvalible;

    private List<DropItem> lootSelected = new();
    public List<DropItem> LootSelected => lootSelected;
    public float ExpWin => expWin;

    private void Start()
    {
        SelectedLoot();
    }

    private void SelectedLoot()
    {
        lootSelected.Clear();
        foreach (DropItem item in lootAvalible)
        {
            float probability = Random.Range(0, 100);
            if (probability <= item.PorcentageDrop)
            {
                lootSelected.Add(item);
            }
        }
    }
}
