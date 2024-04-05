using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class ItemSlot 
{
    public ItemData item;
}
public class UI_Inventory : UI_Base<UI_Inventory>
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public ItemSlotUI[] equippedSlots;
    public ItemSlot[] equipSlots;

    public Text[] selectedItemStatName;
    public Text[] selectedItemStatValue;
    public Text goldText;
    
    private int curEquipIndex;
    private Outline _outline;


    public static UI_Inventory instance;

    void Awake()
    {
        instance = this;

        slots = new ItemSlot[uiSlots.Length];
        equipSlots = new ItemSlot[equippedSlots.Length];

        for (int i = 0; i < uiSlots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            equipSlots[i] = new ItemSlot();
            equippedSlots[i].index = i;
            equippedSlots[i].Clear();
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();

        goldText.text = Managers.UserData.playerGold.ToString();
        UpdateEquipSlots();
        UpdateItemUI();
        UpdateUI();
    }

    public void AddItem(ItemData item)  // 아이템 획득
    {
       
        ItemSlot emptySlot = GetEmptySlot(); // 빈슬롯을 설정
        
        if (emptySlot != null) // 만약 비었다면
        {

            emptySlot.item = item; // 거기에 아이템 추가
            UpdateUI(); // UI업데이트 한번 해주기
            return;

        }
    }

    void UpdateUI()
    {
       
        for (int i = 0; i < slots.Length; i++)
        {

            if (slots[i].item != null)
            {
                uiSlots[i].Set(slots[i]);

            }
            else
                uiSlots[i].Clear();
        }

        for (int i = 0; i < equipSlots.Length; i++)
        {

            if (equipSlots[i].item != null) 
            {
                equippedSlots[i].Set(equipSlots[i]);

            }
            else
                equippedSlots[i].Clear();
        }


    }

    ItemSlot GetEmptySlot() 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }

        return null;
    }

    public void OnEquipButton()
    {
        Managers.UserData.EquipItem(Managers.UserData.playerInventoryItemData[Managers.UserData.inventoryIndex]);
        uiSlots[Managers.UserData.inventoryIndex].icon.gameObject.SetActive(false);
        slots[Managers.UserData.inventoryIndex].item = null;

        UpdateEquipSlots();
        UpdateUI();
        UpdateItemUI();
        Managers.PlayerEquipStatsManager.EquipItemStatsUpdate();
    }

    public void UnEquip()
    {

        Managers.UserData.UnEquipItem(equipSlots[Managers.UserData.equipItemIndex].item);

        equippedSlots[Managers.UserData.equipItemIndex].icon.gameObject.SetActive(false);
        equipSlots[Managers.UserData.equipItemIndex].item = null;

        UpdateEquipSlots();
        UpdateUI();
        UpdateItemUI();
        Managers.PlayerEquipStatsManager.EquipItemStatsUpdate();
    }

    public void UpdateItemUI()
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            slots[i].item = null;
            uiSlots[i].icon.sprite = null;
            if (uiSlots[i].icon.sprite == null)
            {
                uiSlots[i].icon.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Managers.UserData.playerInventoryItemData.Count &&
               Managers.UserData.playerInventoryItemData[i].Sprite != uiSlots[i].icon.sprite)

                AddItem(Managers.UserData.playerInventoryItemData[i]);
         
        }
    }

 

    private void UpdateEquipSlots()
    {

        for (int i = 0; i < equippedSlots.Length; i++)
        {

            if (Managers.UserData.playerEquipItemDatas.ContainsKey(ItemType.Weapon))
            {
                equippedSlots[0].icon.sprite = Managers.UserData.playerEquipItemDatas[ItemType.Weapon].Sprite;
                equipSlots[0].item = Managers.UserData.playerEquipItemDatas[ItemType.Weapon];
            }
            if (Managers.UserData.playerEquipItemDatas.ContainsKey(ItemType.Armor))
            {
                equippedSlots[1].icon.sprite = Managers.UserData.playerEquipItemDatas[ItemType.Armor].Sprite;
                equipSlots[1].item = Managers.UserData.playerEquipItemDatas[ItemType.Armor];
            }
            if (Managers.UserData.playerEquipItemDatas.ContainsKey(ItemType.Shield))
            {
                equippedSlots[2].icon.sprite = Managers.UserData.playerEquipItemDatas[ItemType.Shield].Sprite;
                equipSlots[2].item = Managers.UserData.playerEquipItemDatas[ItemType.Shield];
            }
            if (Managers.UserData.playerEquipItemDatas.ContainsKey(ItemType.Boots))
            {
                equippedSlots[3].icon.sprite = Managers.UserData.playerEquipItemDatas[ItemType.Boots].Sprite;
                equipSlots[3].item = Managers.UserData.playerEquipItemDatas[ItemType.Boots];
            }
            
        }
    }

}
