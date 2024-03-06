using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    public DropItem ItemForCollet { get; set; }

    public void ConfigureLootItem(DropItem dropItem)
    {
        ItemForCollet = dropItem;
        itemIcon.sprite = dropItem.Item.Icon;
        itemName.text = $"{dropItem.Item.Name} x {dropItem.Amount}";
    }

    public void RecogerItem()
    {
        if (ItemForCollet == null) return;
        Inventario.Instance.AddItem(ItemForCollet.Item,ItemForCollet.Amount);
        ItemForCollet.ItemPickedUP = true;
        Destroy(gameObject);
    }
}
