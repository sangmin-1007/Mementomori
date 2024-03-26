using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    Storage,
    Shop,
    Equip
}
public class ItemSlotUI : MonoBehaviour
{
  
    public Button button;
    public Image icon;
   
    private ItemSlot curSlot;
    private Outline outline;

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
    public void OnButtonClick()
    {
        switch(slotType)
        {
            case SlotType.Inventory:
                Managers.DataManager.inventoryIndex = index;
                break;
            case SlotType.Storage:
                Managers.DataManager.storageIndex = index;
                break;
            case SlotType.Shop:
                Managers.DataManager.shopIndex = index;
                break;
            case SlotType.Equip:
                break;
            default:
                break;
        }

    }

    public int OnClickItemSlotButton()
    {
        return index;
    }
}
