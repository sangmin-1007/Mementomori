using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class UI_Storage : UI_Base<UI_Storage>
{
    private ItemSlot[] inventoryItemData;
    private ItemSlot[] storageItemData;

    [SerializeField] private ItemSlotUI[] inventorySlotUI;
    [SerializeField] private ItemSlotUI[] storageSlotUI;

    private int inventoryIndex;
    private int storageIndex;

    private void Awake()
    {
        inventoryItemData = new ItemSlot[inventorySlotUI.Length];
        storageItemData = new ItemSlot[storageSlotUI.Length];

        for(int i = 0; i < inventorySlotUI.Length; i++)
        {
            inventoryItemData[i] = new ItemSlot();
            storageItemData[i] = new ItemSlot();

            inventorySlotUI[i].index = i;
            storageSlotUI[i].index = i;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        UpdateItemData();
    }

    public void OnClickKeepButton()
    {
        inventoryIndex = Managers.DataManager.inventoryIndex;

        if (inventoryItemData[inventoryIndex].item == null) return;

        Managers.DataManager.StorageKeepItemData(inventoryItemData[inventoryIndex].item);
        UpdateItemData();

        Managers.DataManager.inventoryIndex = 0;

    }

    public void OnClickTakeOutButton()
    {
        storageIndex = Managers.DataManager.storageIndex;

        if (storageItemData[storageIndex].item == null) return;

        Managers.DataManager.StorageTakeOutItemData(storageItemData[storageIndex].item);
        UpdateItemData();

        Managers.DataManager.storageIndex = 0;
    }


    public void UpdateItemData()
    {
        for (int i = 0; i < inventorySlotUI.Length; i++)
        {
            inventoryItemData[i].item = null;
            inventorySlotUI[i].icon.sprite = null;
            storageItemData[i].item = null;
            storageSlotUI[i].icon.sprite = null;

            if (inventorySlotUI[i].icon.sprite == null)
            {
                inventorySlotUI[i].icon.gameObject.SetActive(false);
            }
            if (storageSlotUI[i].icon.sprite == null)
            {
                storageSlotUI[i].icon.gameObject.SetActive(false);
            }
        }

        for(int i = 0; i < inventoryItemData.Length; i++)
        {
            if (Managers.DataManager.playerInventoryItemData != null && i < Managers.DataManager.playerInventoryItemData.Count)
            {
                inventoryItemData[i].item = Managers.DataManager.playerInventoryItemData[i];
                inventorySlotUI[i].icon.sprite = inventoryItemData[i].item.Sprite;
                if (inventorySlotUI[i].icon.sprite != null)
                    inventorySlotUI[i].icon.gameObject.SetActive(true);
            }

            if(Managers.DataManager.storageItemData != null && i < Managers.DataManager.storageItemData.Count)
            {
                storageItemData[i].item = Managers.DataManager.storageItemData[i];
                storageSlotUI[i].icon.sprite = storageItemData[i].item.Sprite;
                if (storageSlotUI[i].icon.sprite != null)
                    storageSlotUI[i].icon.gameObject.SetActive(true);
            }
        }
    }
}
