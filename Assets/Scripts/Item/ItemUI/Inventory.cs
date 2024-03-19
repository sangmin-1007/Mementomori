using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;


public class ItemSlot 
{
    public ItemData item;
}
public class Inventory : UI_Base<Inventory>
{
    public ItemSlotUI[] uiSlots;
    public ItemSlotUI[] equippedSlots;
    public ItemSlot[] slots;
   
    public GameObject inventoryWindow;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    
    public Text selectedItemName;
    public Text selectedItemDescription;

    public Text selectedItemStatAtk;
    public Text selectedItemStatHp;
    public Text selectedItemStatAtk_Speed;
    public Text selectedItemStatDef;
    public Text selectedItemStatSpeed;
    public Text selectedItemStatStamina;
    public Text selectedItemStatValue_Atk;
    public Text selectedItemStatValue_Atk_speed;
    public Text selectedItemStatValue_Hp;
    public Text selectedItemStatValue_Speed;
    public Text selectedItemStatValue_Def;
    public Text selectedItemStatValue_Stamina;

    public GameObject equipButton;
    public GameObject unequipButton;
 

  


    private SpriteRenderer spriteRenderer;
  

    private int curEquipIndex;

    //private PlayerController controller; 캐릭터 InvokeEvent방식이랑 호환
    //private PlayerCondition condition; 장비창이 스탯이랑 연관있으니 캐릭터 스탯?이랑 호환해야할듯

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    void Awake()
    {
       
        instance = this;
        //private PlayerController controller; 캐릭터 InvokeEvent방식이랑 호환
        //private PlayerCondition condition; 장비창이 스탯이랑 연관있으니 캐릭터 스탯?이랑 호환해야할듯

    }

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < uiSlots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        ClearSeletecItemWindow();

       

    }

   
   
    
    public void Toggle()
    {
        if (inventoryWindow.activeInHierarchy)
        {
            inventoryWindow.SetActive(false);
            onCloseInventory?.Invoke();
            //controller.ToggleCursor(false); 공격키가 클릭일경우
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            //controller.ToggleCursor(true); 공격키가 클릭일경우
        }
    }


    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)  // 아이템 획득
    {
       
        ItemSlot emptySlot = GetEmptySlot(); // 빈슬롯을 설정
        
        if (emptySlot != null) // 만약 비었다면
        {

            emptySlot.item = item; // 거기에 아이템 추가
            Managers.DataManager.AddItem(item);
            UpdateUI(); // UI업데이트 한번 해주기
            return;

        }
    }
   



    //void ThrowItem(ItemData item) 버리기 기능있을때
    //{

    //}

    public void RemoveSelectedItem()
    {
       if (uiSlots[selectedItemIndex].equipped)
       {
          UnEquip(selectedItemIndex);
       }

         selectedItem.item = null;
         ClearSeletecItemWindow();
         UpdateUI();


      
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

    public void SelectItem(int index)
    {
        if (slots[index].item == null)
            return;
       
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.Name;
        selectedItemDescription.text = selectedItem.item.Description;


        if (selectedItem.item.Atk > 0) // 아이템이 검일때 검은 공격력/공격속도만 보여준다.
        {
            selectedItemStatHp.gameObject.SetActive(false);
            selectedItemStatDef.gameObject.SetActive(false);
            selectedItemStatSpeed.gameObject.SetActive(false);
            selectedItemStatStamina.gameObject.SetActive(false);
            selectedItemStatAtk.gameObject.SetActive(true);
            selectedItemStatValue_Atk.gameObject.SetActive(true);
            selectedItemStatValue_Atk.text += selectedItem.item.Atk.ToString() + "\n";
            selectedItemStatAtk_Speed.gameObject.SetActive(true);
            selectedItemStatValue_Atk_speed.gameObject.SetActive(true);
            selectedItemStatValue_Atk_speed.text += selectedItem.item.AtkSpeed.ToString() + "\n";

        }

        if(selectedItem.item.Hp > 0) // 갑옷 = 체력
        {
            selectedItemStatAtk.gameObject.SetActive(false);
            selectedItemStatAtk_Speed.gameObject.SetActive(false);
            selectedItemStatDef.gameObject.SetActive(false);
            selectedItemStatSpeed.gameObject.SetActive(false);
            selectedItemStatStamina.gameObject.SetActive(false);
            selectedItemStatHp.gameObject.SetActive(true);
            selectedItemStatValue_Hp.gameObject.SetActive(true);
            selectedItemStatValue_Hp.text += selectedItem.item.Hp.ToString() + "\n";
        }

        if(selectedItem.item.Def > 0) // 방패 = 방어력
        {
            selectedItemStatAtk.gameObject.SetActive(false);
            selectedItemStatHp.gameObject.SetActive(false);
            selectedItemStatAtk_Speed.gameObject.SetActive(false);
            selectedItemStatSpeed.gameObject.SetActive(false);
            selectedItemStatStamina.gameObject.SetActive(false);
            selectedItemStatDef.gameObject.SetActive(true);
            selectedItemStatValue_Def.gameObject.SetActive(true);
            selectedItemStatValue_Def.text += selectedItem.item.Def.ToString() + "\n";
        }

        if(selectedItem.item.Speed > 0) // 신발 = 스태미나 + 이동속도
        {
            selectedItemStatAtk.gameObject.SetActive(false);
            selectedItemStatHp.gameObject.SetActive(false);
            selectedItemStatAtk_Speed.gameObject.SetActive(false);
            selectedItemStatDef.gameObject.SetActive(false);
            selectedItemStatStamina.gameObject.SetActive(true);
            selectedItemStatValue_Stamina.gameObject.SetActive(true);
            selectedItemStatValue_Stamina.text += selectedItem.item.Stamina.ToString() + "\n";
            selectedItemStatSpeed.gameObject.SetActive(true);
            selectedItemStatValue_Speed.gameObject.SetActive(true);
            selectedItemStatValue_Speed.text += selectedItem.item.Speed.ToString() + "\n";
        }
       

        equipButton.SetActive(!uiSlots[index].equipped);
        unequipButton.SetActive(uiSlots[index].equipped);
       

    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatAtk.gameObject.SetActive(false);
        selectedItemStatHp.gameObject.SetActive(false);
        selectedItemStatAtk_Speed.gameObject.SetActive(false);
        selectedItemStatDef.gameObject.SetActive(false);
        selectedItemStatSpeed.gameObject.SetActive(false); 
        selectedItemStatStamina.gameObject.SetActive(false);
        selectedItemStatValue_Atk.text = string.Empty;
        selectedItemStatValue_Atk_speed.text = string.Empty;
        selectedItemStatValue_Hp.text = string.Empty;
        selectedItemStatValue_Speed.text = string.Empty;
        selectedItemStatValue_Def.text = string.Empty;
        selectedItemStatValue_Stamina.text = string.Empty;


        equipButton.SetActive(false);
        unequipButton.SetActive(false);
      
    }


    //============버튼=======================

    public void OnEquipButton()
    {
        if (uiSlots[curEquipIndex].equipped)
        {
            UnEquip(curEquipIndex);
        }
     
        uiSlots[selectedItemIndex].equipped = true;
        curEquipIndex = selectedItemIndex;

        //if (selectedItem.item.Type == ItemType.Weapon)
        //{
        //    uiSlots[selectedItemIndex] = equippedSlots[0];
          
        //}
        //else if (selectedItem.item.Type == ItemType.Armor)
        //{
        //    uiSlots[selectedItemIndex] = equippedSlots[1];
        //}
        //else if (selectedItem.item.Type == ItemType.Shield)
        //{
        //    uiSlots[selectedItemIndex] = equippedSlots[2];
        //}
        //else if (selectedItem.item.Type == ItemType.Boots)
        //{
        //    uiSlots[selectedItemIndex] = equippedSlots[3];
        //}
        
        
        UpdateUI();

        SelectItem(selectedItemIndex);
    }

    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
        
        UpdateUI();

        if (selectedItemIndex == index)
            SelectItem(index);
    }

    public void OnUnequipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public bool HasItems(ItemData item)
    {
        return false;
    }
}
