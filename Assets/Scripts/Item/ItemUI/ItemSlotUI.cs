using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    Inventory,
    Equip
 
}



public class ItemSlotUI : MonoBehaviour,IPointerClickHandler , IPointerEnterHandler, IPointerExitHandler//IBeginDragHandler,IDragHandler, IEndDragHandler,IDropHandler
{
    public SlotType slotType;
    
    public ItemData item;
    public Button button;
    public Image icon;
   
    private ItemSlot curSlot;
    private Outline outline;


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


    public void SetColor(float _alpha)
    {
        Color color = icon.color;
        color.a = _alpha;
        icon.color = color;
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

    public void AddItemSlot(ItemData _item)
    {
      
        _item = item;
        icon.sprite = item.Sprite;
       
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
      
        
    }

  
    public void Clear()
    {
        curSlot = null;
        //icon.gameObject.SetActive(false);
    }
    public void OnButtonClick()
    {
        //Inventory.instance.SelectItem(index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      switch(slotType)
        {
            case SlotType.Inventory:
                Inventory.instance.SelectItem(index, transform.position);
                break;

            case SlotType.Equip:
                Inventory.instance.SelectEquipItem(index, transform.position);
                break;
        }
       
 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        Inventory.instance.ClearSeletecItemWindow();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
         
                Inventory.instance.OnEquipButton();
            
        }
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{

    //    if (item != null)
    //    {
    //        DragSlot.instance.dragSlot = this;
    //        DragSlot.instance.DragSetImage(icon);
    //        DragSlot.instance.transform.position = eventData.position;
    //    }

    //}
    //public void OnDrag(PointerEventData eventData)
    //{
    //    if(item != null)
    //    {
    //        DragSlot.instance.transform.position = eventData.position;
    //    }

    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    DragSlot.instance.SetColor(0);
    //    DragSlot.instance.dragSlot = null;
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    if(DragSlot.instance.dragSlot.item != null)
    //    {
    //        ChageSlot();
    //    }



    //}

    //private void ChageSlot<ItemData>(this List<ItemData> list, int from, int to)
    //{ 
    //        ItemData tmp = list[from];
    //        list[from] = list[to];
    //        list[to] = tmp;


    //     ItemData _tempItem = item;
    //    AddItemSlot(DragSlot.instance.dragSlot.item);

    //    if (_tempItem != null)
    //    {
    //        Debug.Log("호호잇");
    //        DragSlot.instance.dragSlot.AddItemSlot(_tempItem);

    //        //Debug.Log("업데이트!");
    //        //Inventory.instance.UpdateItemUI();
    //    }
    //    else
    //    {
    //        Debug.Log("dfdf");
    //        DragSlot.instance.dragSlot.ClearSlot();
    //    }
    //}
}
