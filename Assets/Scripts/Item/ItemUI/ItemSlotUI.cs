    using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    Storage,
    Storage_Inventory,
    Shop,
    Shop_Inventory,
    Equip
}
public class ItemSlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
  
    public Button button;
    public Image icon;
   
    public ItemSlot curSlot;
    private Outline outline;

    private UI_ItemToolTip toolTip;
    private UI_Storage storage;
    private UI_Shop shop;

    public SlotType slotType;
    public int index;
    public bool equipped;


    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
        button.onClick.AddListener(OnClickSlot);
    }

    
    public void Set(ItemSlot slot)
    {
        
        curSlot = slot;
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.Sprite; // 먹은 아이템 스프라이트 불러옴;;
        
        if (outline != null)
        {
            outline.enabled = equipped;
       
        }

    }

  
    public void Clear()
    {
        curSlot = null;
        icon.gameObject.SetActive(false);
     
    }

    public void OnClickSlot()
    {
        switch(slotType)
        {
            case SlotType.Inventory:
                Managers.UserData.inventoryIndex = index;
                break;
            case SlotType.Storage:
                Managers.UserData.storageIndex = index;
                break;
            case SlotType.Storage_Inventory:
                Managers.UserData.inventoryIndex = index;
                break;
            case SlotType.Equip:
                Managers.UserData.equipItemIndex = index;
                break;
            case SlotType.Shop:
                Managers.UserData.shopIndex = index;
                break;
            case SlotType.Shop_Inventory:
                Managers.UserData.inventoryIndex = index;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Managers.UserData.selectSlotType = slotType;
        Managers.UI_Manager.ShowUI<UI_ItemToolTip>();

        if(Managers.UI_Manager.UI_List.ContainsKey("UI_ItemToolTip"))
        {
            toolTip = Managers.UI_Manager.UI_List["UI_ItemToolTip"].GetComponent<UI_ItemToolTip>();
        }

        switch (slotType)
        {
            case SlotType.Inventory:
                Managers.UserData.inventoryIndex = index;
                toolTip.ItemInfoText(curSlot.item,transform.position);
                break;
            case SlotType.Equip:
                Managers.UserData.equipItemIndex = index;
                toolTip.ItemInfoText(curSlot.item, transform.position);
                break;
            case SlotType.Storage:
                Managers.UserData.storageIndex = index;
                toolTip.ItemInfoText(curSlot.item, transform.position);
                break;
            case SlotType.Storage_Inventory:
                Managers.UserData.inventoryIndex = index;
                toolTip.ItemInfoText(curSlot.item, transform.position);
                break;
            case SlotType.Shop:
                Managers.UserData.shopIndex = index;
                toolTip.ItemInfoText(curSlot.item, transform.position);
                break;
            case SlotType.Shop_Inventory:
                Managers.UserData.inventoryIndex = index;
                toolTip.ItemInfoText(curSlot.item, transform.position);
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Managers.UI_Manager.IsActive<UI_Storage>())
        {
            storage = Managers.UI_Manager.UI_List["UI_Storage"].GetComponent<UI_Storage>();
        }

        if(Managers.UI_Manager.IsActive<UI_Shop>())
        {
            shop = Managers.UI_Manager.UI_List["UI_Shop"].GetComponent<UI_Shop>();
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            switch(slotType)
            {
                case SlotType.Inventory:
                    UI_Inventory.instance.OnEquipButton();
                    break;
                case SlotType.Storage:
                    storage.OnClickTakeOutButton();
                    break;
                case SlotType.Storage_Inventory:
                    storage.OnClickKeepButton();
                    break;
                case SlotType.Shop:
                    shop.OnClickBuyButton();
                    break;
                case SlotType.Shop_Inventory:
                    shop.OnClickSellButton();
                    break;
                case SlotType.Equip:
                    UI_Inventory.instance.UnEquip();
                    break;
            }
            
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Managers.UI_Manager.HideUI<UI_ItemToolTip>();
    }
}
