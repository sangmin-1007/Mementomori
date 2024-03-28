using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : UI_Base<UI_Shop>
{
    [SerializeField] private ItemSlot[] storeItemData;
    [SerializeField] private ItemSlot[] inventoryItemData;

    [SerializeField] private ItemSlotUI[] storeSlotUI;
    [SerializeField] private ItemSlotUI[] inventorySlotUI;

    private void Awake()
    {
        storeItemData = new ItemSlot[storeSlotUI.Length];
        inventoryItemData = new ItemSlot[inventorySlotUI.Length];

        for(int i = 0; i < storeSlotUI.Length; i++)
        {
            storeItemData[i] = new ItemSlot();
            storeSlotUI[i].index = i;
            storeSlotUI[i].icon.gameObject.SetActive(false);
        }

        for(int i = 0; i < inventorySlotUI.Length; i++)
        {
            inventoryItemData[i] = new ItemSlot();
            inventorySlotUI[i].index = i;
            inventorySlotUI[i].icon.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        UpdateStore();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        UpdateInventory();
    }

    private void UpdateStore()
    {
        int itemID = 0;

        for(int i = 0; i < storeItemData.Length; i++)
        {
            itemID = DataBase.Item.GetRandomItemID();
            storeItemData[i].item = DataBase.Item.GetID(itemID);
            storeSlotUI[i].icon.sprite = storeItemData[i].item.Sprite;

            if (storeSlotUI[i].icon.sprite != null)
                storeSlotUI[i].icon.gameObject.SetActive(true);
        }
    }

    private void UpdateInventory()
    {
        for(int i = 0; i < inventoryItemData.Length; i++)
        {
            if (i < Managers.DataManager.playerInventoryItemData.Count)
            {
                inventoryItemData[i].item = Managers.DataManager.playerInventoryItemData[i];
                inventorySlotUI[i].icon.sprite = inventoryItemData[i].item.Sprite;
            }

            if (inventorySlotUI[i].icon.sprite != null)
                inventorySlotUI[i].icon.gameObject.SetActive(true);
        }
    }

}
