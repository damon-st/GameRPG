using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{

    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;


    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    public int IndexSlotInitForMove { get; private set; }
    List<InventarioSlot> slotsAvalibles = new();
    public InventarioSlot SlotSelected { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        InitializedInventario();
        IndexSlotInitForMove = -1;
    }

    private void Update()
    {
        UpdateSlotSelected();
        if(Input.GetKeyDown(KeyCode.M))
        {
            if (SlotSelected != null)
            {
                IndexSlotInitForMove = SlotSelected.Index;
            }
        }
    }

    private void InitializedInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumberSlots; i++)
        {
         InventarioSlot newSlot=   Instantiate(slotPrefab, contenedor);
            newSlot.Index = i;
           slotsAvalibles.Add(newSlot);
        }
    }


    private void UpdateSlotSelected()
    {
        GameObject goSelected = EventSystem.current.currentSelectedGameObject;
        if (goSelected == null) return;

        InventarioSlot slot = goSelected.GetComponent<InventarioSlot>();
        if (slot != null)
        {
            SlotSelected = slot;
        }
    }


    public void DrawItemInventario(InventarioItem itemForAdd,int amount,int itemIndex) 
    {
        InventarioSlot slot = slotsAvalibles[itemIndex];
        if (itemForAdd != null)
        {
            slot.ActiveSlotUI(true);
            slot.UpdateSlot(itemForAdd, amount);
        }
        else
        {
            slot.ActiveSlotUI(false);
        }

    }

    

    private void UpdateInventarioDescription(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            var item = Inventario.Instance.ItemsInventario[index];
            itemIcon.sprite = item.Icon;
            itemName.text = item.Name;
            itemDescription.text = item.Description;
            panelInventarioDescription.SetActive(true);
        }
        else
        {
            panelInventarioDescription.SetActive(false);
        }
    }


    public void UseItem()
    {
        if (SlotSelected != null)
        {
            SlotSelected.SlotUseItem();
            SlotSelected.SelectSlot();
        }
    }

    public void EquipToItem()
    {
        if (SlotSelected != null)
        {
            SlotSelected.SlotToEquipItem();
            SlotSelected.SelectSlot();
        }
    }

    public void RemoveItem()
    {
        if (SlotSelected != null)
        {
            SlotSelected.SlotRemoveItem();
            SlotSelected.SelectSlot();
        }
    }

    #region

    private void SlotInteractionResponse(TypeInteracction type, int index)
    {
        if (type == TypeInteracction.Click)
        {
            UpdateInventarioDescription(index);
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventSlotInteraction += SlotInteractionResponse;
    }

    private void OnDisable()
    {
        InventarioSlot.EventSlotInteraction -= SlotInteractionResponse;
    }

    #endregion

}
