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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Managers.DataManager.selectSlotType = slotType;
        Managers.UI_Manager.ShowUI<UI_ItemToolTip>();

        if(Managers.UI_Manager.UI_List.ContainsKey("UI_ItemToolTip"))
        {
            toolTip = Managers.UI_Manager.UI_List["UI_ItemToolTip"].GetComponent<UI_ItemToolTip>();
        }

        switch (slotType)
        {
            case SlotType.Inventory:
                Managers.DataManager.inventoryIndex = index;
                toolTip.ItemInfoText(curSlot.item);
                //Inventory.instance.SelectItem(index, transform.position);
                break;
            case SlotType.Equip:
                Managers.DataManager.equipItemIndex = index;
                toolTip.ItemInfoText(curSlot.item);
                //Inventory.instance.SelectEquipItem(index, transform.position);
                break;
            case SlotType.Storage:
                Managers.DataManager.storageIndex = index;
                toolTip.ItemInfoText(curSlot.item);
                break;
            case SlotType.Storage_Inventory:
                Managers.DataManager.inventoryIndex = index;
                toolTip.ItemInfoText(curSlot.item);
                break;
            case SlotType.Shop:
                break;
            case SlotType.Shop_Inventory:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(Managers.UI_Manager.UI_List.ContainsKey("UI_Storage"))
        {
            storage = Managers.UI_Manager.UI_List["UI_Storage"].GetComponent<UI_Storage>();
        }
        else if(Managers.UI_Manager.UI_List.ContainsKey("UI_Shop"))
        {
            shop = Managers.UI_Manager.UI_List["UI_Shop"].GetComponent<UI_Shop>();
        }

        if(eventData.button == PointerEventData.InputButton.Right)
        {
            switch(slotType)
            {
                case SlotType.Inventory:
                    Inventory.instance.OnEquipButton();
                    break;
                case SlotType.Storage:
                    storage.OnClickTakeOutButton();
                    break;
                case SlotType.Storage_Inventory:
                    storage.OnClickKeepButton();
                    break;
                case SlotType.Shop:
                    break;
                case SlotType.Equip:
                    Inventory.instance.UnEquip();
                    break;
            }
            
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Managers.UI_Manager.HideUI<UI_ItemToolTip>();
        //Inventory.instance.ClearSeletecItemWindow();
    }
}
