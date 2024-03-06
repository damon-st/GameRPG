using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TypeInteracction
{
    Click,
    Use,
    ToEquip,
    Remove,
}

public class InventarioSlot : MonoBehaviour
{

    public static Action<TypeInteracction, int> EventSlotInteraction;

    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject fondoAmount;
    [SerializeField] private TextMeshProUGUI amoutTMP;
    public int Index { get; set; }

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }


    public void UpdateSlot(InventarioItem item,int amout)
    {
        itemIcon.sprite = item.Icon;
        amoutTMP.text = amout.ToString();
    }

    public void ActiveSlotUI(bool status)
    {
        itemIcon.gameObject.SetActive(status);
        fondoAmount.SetActive(status);
    }

    public void SelectSlot()
    {
        _button.Select();
    }


    public void ClickSlot()
    {
        EventSlotInteraction?.Invoke(TypeInteracction.Click, Index);

        //Move item
        if (InventarioUI.Instance.IndexSlotInitForMove != -1)
        {
            if (InventarioUI.Instance.IndexSlotInitForMove != Index)
            {
                //MOVE
                Inventario.Instance.MoveItem(InventarioUI.Instance.IndexSlotInitForMove, Index);
            }
        }
    }

    public void SlotUseItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventSlotInteraction?.Invoke(TypeInteracction.Use, Index);
        }
    }


    public void SlotToEquipItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventSlotInteraction?.Invoke(TypeInteracction.ToEquip, Index);
        }
    }

    public void SlotRemoveItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventSlotInteraction?.Invoke(TypeInteracction.Remove, Index);
        }
    }
}
