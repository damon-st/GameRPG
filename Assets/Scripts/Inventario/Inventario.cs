using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{

    [Header("Items")]
    [SerializeField] private InventarioAlmacen inventarioAlmacen;
    [SerializeField] private Character character;
    [SerializeField] private int numberSlots;
    [SerializeField] private InventarioItem[] itemsInventario;

    public Character Character => character;

    public int NumberSlots => numberSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private readonly string INVENTARIO_KEY = "MIJUEGOINVENTARIO";

    private void Start()
    {
        itemsInventario = new InventarioItem[numberSlots];
        LoadInventario();
    }


    public void AddItem(InventarioItem itemForAdd, int amount)
    {
        if (itemForAdd == null) return;
        //Verify  in case exist in inventario 
        List<int> indexes = VerifyExistency(itemForAdd.ID);
        if (itemForAdd.IsAccumulation)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    var item = itemsInventario[indexes[i]];
                    if (item.Amount < itemForAdd.AccumulationMax)
                    {
                        item.Amount += amount;
                        if (item.Amount > itemForAdd.AccumulationMax)
                        {
                           int difference = item.Amount- itemForAdd.AccumulationMax;
                            item.Amount = itemForAdd.AccumulationMax;
                            AddItem(itemForAdd, difference);
                        }

                        InventarioUI.Instance.DrawItemInventario(itemForAdd, item.Amount, indexes[i]);
                        return;
                    }
                }
            }
        }

        if (amount <= 0) return;

        if (amount > itemForAdd.AccumulationMax)
        {
            AddItemInSlotAvalibled(itemForAdd, itemForAdd.AccumulationMax);
            amount -= itemForAdd.AccumulationMax;
            AddItem(itemForAdd, amount);
        }
        else
        {
            AddItemInSlotAvalibled(itemForAdd, amount);
        }
        SaveInventario();
    }


    private List<int> VerifyExistency(string itemID)
    {
        List<int> indexesItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null) continue;
            if (itemsInventario[i].ID == itemID)
            {
                indexesItem.Add(i);
            }
        }
        return indexesItem;
    }

    public int GetAmountOfItem(string itemID) 
    {
        List<int> indexes = VerifyExistency(itemID);
        int amountTotal = 0;
        foreach (var index in indexes)
        {
            if (itemsInventario[index].ID == itemID)
            {
                amountTotal += itemsInventario[index].Amount;
            }
        }

        return amountTotal;
    }

    public void ConsumeItem(string itemID) 
    {
        List<int> indexes = VerifyExistency(itemID);
        if (indexes.Count > 0)
        {
            DeleteItem(indexes[indexes.Count - 1]);
        }

    }

    private void AddItemInSlotAvalibled(InventarioItem item,int amount)
    {
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopyItem();
                itemsInventario[i].Amount = amount;
                InventarioUI.Instance.DrawItemInventario(item, amount, i);
                return;
            }
        }
    }

    private void DeleteItem(int index)
    {
        itemsInventario[index].Amount--;

        if (itemsInventario[index].Amount <= 0)
        {
            itemsInventario[index].Amount = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DrawItemInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DrawItemInventario(itemsInventario[index], itemsInventario[index].Amount, index);
        }
        SaveInventario();
    }

    public void MoveItem(int indexInitial,int indexFinal) {

        if (itemsInventario[indexInitial] == null || itemsInventario[indexFinal] != null) return;

        //copi item in slot final
        InventarioItem itemForMove = itemsInventario[indexInitial].CopyItem();
        itemsInventario[indexFinal]=itemForMove;
        InventarioUI.Instance.DrawItemInventario(itemForMove,itemForMove.Amount, indexFinal);

        //remove item for slot inicial
        itemsInventario[indexInitial] = null;
        InventarioUI.Instance.DrawItemInventario(null, 0, indexInitial);

        SaveInventario();
    }

    private void UseItem(int index)
    {
        if (itemsInventario[index] == null) return;
        if (itemsInventario[index].UseItem())
        {
            DeleteItem(index);
        }
    }

    private void EquipToItem(int index)
    {
        if (itemsInventario[index]==null) return;
        if (itemsInventario[index].TypeItem != TypesItems.Weapons) return;
        itemsInventario[index].ToEquipItem();
    }

    private void RemoveItem(int index)
    {
        if (itemsInventario[index] == null) { return; }
        if (itemsInventario[index].TypeItem != TypesItems.Weapons) return;
        itemsInventario[index].RemoveItem();
    }

    #region Guardado

    private InventarioItem ItemExitsInAlamacen(string ID)
    {

        for (int i = 0; i < inventarioAlmacen.Items.Length; i++)
        {
            if (inventarioAlmacen.Items[i].ID == ID)
            {
                return inventarioAlmacen.Items[i];
            }
        }
        return null;
    }

    private void SaveInventario() 
    {
        InventarioData dataSave= new InventarioData();
        dataSave.ItemsData = new string[numberSlots];
        dataSave.ItemsAmount = new int[numberSlots];

        for (int i = 0; i < numberSlots; i++)
        {
            if (itemsInventario[i]==null || string.IsNullOrEmpty(itemsInventario[i].ID))
            {
                dataSave.ItemsData[i] = null;
                dataSave.ItemsAmount[i] = 0;
            }
            else
            {
                dataSave.ItemsData[i] = itemsInventario[i].ID;
                dataSave.ItemsAmount[i] = itemsInventario[i].Amount;
            }
        }
        SaveGame.Save(INVENTARIO_KEY, dataSave);
    }
    private void LoadInventario() 
    {
        if (!SaveGame.Exists(INVENTARIO_KEY)) return;
        InventarioData dataLoad = SaveGame.Load<InventarioData>(INVENTARIO_KEY);
        for (int i = 0; i < numberSlots; i++)
        {
           if (dataLoad.ItemsData[i] != null)
            {
                InventarioItem itemAlamcen = ItemExitsInAlamacen(dataLoad.ItemsData[i]);
                if(itemAlamcen != null)
                {
                    itemsInventario[i] = itemAlamcen.CopyItem();
                    itemsInventario[i].Amount = dataLoad.ItemsAmount[i];
                    InventarioUI.Instance.DrawItemInventario(itemsInventario[i], itemsInventario[i].Amount, i);
                }
            }
           else
            {
                itemsInventario[i] = null;
            }
        }
    }

    #endregion

    #region Eventos

    private void SlotInteractionResponse(TypeInteracction type, int index)
    {
        switch (type)
        {
            case TypeInteracction.Use:
                UseItem(index);
                break;
            case TypeInteracction.ToEquip:
                EquipToItem(index);
                break;
            case TypeInteracction.Remove:
                RemoveItem(index);
                break;
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
