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
        inventoryIndex = Managers.UserData.inventoryIndex;

        if (inventoryItemData[inventoryIndex].item == null) return;

        Managers.UserData.StorageKeepItemData(inventoryItemData[inventoryIndex].item);
        UpdateItemData();

        Managers.UserData.inventoryIndex = 0;

    }

    public void OnClickTakeOutButton()
    {
        storageIndex = Managers.UserData.storageIndex;

        if (storageItemData[storageIndex].item == null) return;

        Managers.UserData.StorageTakeOutItemData(storageItemData[storageIndex].item);
        UpdateItemData();

        Managers.UserData.storageIndex = 0;
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
            if (Managers.UserData.playerInventoryItemData != null && i < Managers.UserData.playerInventoryItemData.Count)
            {
                inventoryItemData[i].item = Managers.UserData.playerInventoryItemData[i];
                inventorySlotUI[i].Set(inventoryItemData[i]);
                inventorySlotUI[i].icon.sprite = inventoryItemData[i].item.Sprite;
                if (inventorySlotUI[i].icon.sprite != null)
                    inventorySlotUI[i].icon.gameObject.SetActive(true);
            }

            if(Managers.UserData.storageItemData != null && i < Managers.UserData.storageItemData.Count)
            {
                storageItemData[i].item = Managers.UserData.storageItemData[i];
                storageSlotUI[i].Set(storageItemData[i]);
                storageSlotUI[i].icon.sprite = storageItemData[i].item.Sprite;
                if (storageSlotUI[i].icon.sprite != null)
                    storageSlotUI[i].icon.gameObject.SetActive(true);
            }
        }
    }
}
