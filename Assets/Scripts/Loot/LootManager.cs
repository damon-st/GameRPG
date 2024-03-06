using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject panelLoot;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform lootContainer;
    
    public void ShowLoot(EnemigoLoot enemigoLoot)
    {
        panelLoot.SetActive(true);
        if (ContainerBusy())
        {
            foreach (Transform child in lootContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }
        for (int i = 0; i < enemigoLoot.LootSelected.Count; i++)
        {
            LoadLootPanel(enemigoLoot.LootSelected[i]);
        }
    }

    public void ClosePanel()
    {
        panelLoot.SetActive(false);
    }

    private void LoadLootPanel(DropItem dropItem)
    {
        if (dropItem.ItemPickedUP) return;
        LootButton loot = Instantiate(lootButtonPrefab, lootContainer);
        loot.ConfigureLootItem(dropItem);
        loot.transform.SetParent(lootContainer);
    }

    private bool ContainerBusy()
    {
        LootButton[] childs = lootContainer.GetComponentsInChildren<LootButton>();
        return childs.Length > 0;
    }
}
