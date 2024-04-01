using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemToolTip : UI_Base<UI_ItemToolTip>
{

    [SerializeField] private Text ItemNameText;
    [SerializeField] private Text descriptionText;

    [SerializeField] private Text inputKeyText;

    [SerializeField] private Text[] selectedItemStat;
    [SerializeField] private Text[] selectedItemStatValue;


    private SlotType selectedSlotType;
    private int selectedItemIndex;

    public override void OnEnable()
    {
        base.OnEnable();

        InputKeyText();
    }

    private void InputKeyText()
    {
        selectedSlotType = Managers.DataManager.selectSlotType;

        switch (selectedSlotType)
        {
            case SlotType.Inventory:
                inputKeyText.text = "우클릭 - 장착";
                break;
            case SlotType.Storage:
                inputKeyText.text = "우클릭 - 보관";
                break;
            case SlotType.Storage_Inventory:
                inputKeyText.text = "우클릭 - 꺼내기";
                break;
            case SlotType.Shop:
                inputKeyText.text = "우클릭 - 구매하기";
                break;
            case SlotType.Shop_Inventory:
                inputKeyText.text = "우클릭 - 판매하기";
                break;
            case SlotType.Equip:
                inputKeyText.text = "우클릭 - 해제";
                break;
        }
    }

    //private void SelectedItem()
    //{


    //    switch(selectedSlotType)
    //    {
    //        case SlotType.Inventory:
    //            selectedItemIndex = Managers.DataManager.inventoryIndex;
    //            ItemNameText.text = Managers.DataManager.playerInventoryItemData[selectedItemIndex].Name;
    //            descriptionText.text = Managers.DataManager.playerInventoryItemData[selectedItemIndex].Description;
    //            break;
    //        case SlotType.Storage:
    //            selectedItemIndex = Managers.DataManager.storageIndex;
    //            ItemNameText.text = Managers.DataManager.storageItemData[selectedItemIndex].Name;
    //            descriptionText.text = Managers.DataManager.storageItemData[selectedItemIndex].Description;
    //            break;
    //        case SlotType.Shop:
    //            selectedItemIndex = Managers.DataManager.shopIndex;
    //            break;
    //        case SlotType.Equip:
    //            selectedItemIndex = Managers.DataManager.equipItemIndex;
    //            break;
    //    }
    //}

    public void ItemInfoText(ItemData itemData)
    {
        ItemNameText.text = itemData.Name;
        descriptionText.text = itemData.Description;

        switch (itemData.Type)
        {
            case ItemType.Weapon:
                selectedItemStat[0].text = "공격력 : ";
                selectedItemStatValue[0].text = itemData.Atk.ToString();
                selectedItemStat[1].text = "공격속도 : ";
                selectedItemStatValue[1].text = itemData.AtkSpeed.ToString();
                break;
            case ItemType.Armor:
                selectedItemStat[0].text = "체력 : ";
                selectedItemStatValue[0].text = itemData.Hp.ToString();
                selectedItemStat[1].text = "";
                selectedItemStatValue[1].text = "";
                break;
            case ItemType.Shield:
                selectedItemStat[0].text = "방어력 : ";
                selectedItemStatValue[0].text = itemData.Def.ToString();
                selectedItemStat[1].text = "";
                selectedItemStatValue[1].text = "";
                break;
            case ItemType.Boots:
                selectedItemStat[0].text = "이동속도 : ";
                selectedItemStatValue[0].text = itemData.Speed.ToString();
                selectedItemStat[1].text = "스태미나 : ";
                selectedItemStatValue[1].text = itemData.Stamina.ToString();
                break;
        }

    }
}
