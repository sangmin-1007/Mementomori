using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using UnityEngine.UIElements;
//using static UnityEditor.Progress;


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

    public Text[] selectedItemStatName;
    public Text[] selectedItemStatValue;

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

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < uiSlots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        ClearSeletecItemWindow();
    }

    public override void OnEnable()
    {
        base.OnEnable();


        UpdateEquipSlots();
        UpdateItemUI();
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
        {
            selectedItemName.gameObject.SetActive(false);
            selectedItemDescription.gameObject.SetActive(false);
            for(int i = 0; i < selectedItemStatName.Length; i++)
            {
                selectedItemStatName[i].gameObject.SetActive(false);
                selectedItemStatValue[i].gameObject.SetActive(false);
            }
            return;
        }

       
        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.Name;
        selectedItemDescription.text = selectedItem.item.Description;

        selectedItemName.gameObject.SetActive(true);
        selectedItemDescription.gameObject.SetActive(true);

        for (int i = 0; i < selectedItemStatName.Length; i++)
        {
            selectedItemStatName[i].gameObject.SetActive(true);
            selectedItemStatValue[i].gameObject.SetActive(true);
        }


        if (selectedItem.item.Type == ItemType.Weapon) // 아이템이 검일때 검은 공격력/공격속도만 보여준다.
        {
            selectedItemStatName[0].text = "공격력 : ";
            selectedItemStatName[1].text = "공격속도 :";

            selectedItemStatValue[0].text = selectedItem.item.Atk.ToString();
            selectedItemStatValue[1].text = selectedItem.item.AtkSpeed.ToString();
        }

        if(selectedItem.item.Type == ItemType.Armor) // 갑옷 = 체력
        {
            selectedItemName.gameObject.SetActive(true);
            selectedItemDescription.gameObject.SetActive(true);

            selectedItemStatName[0].text = "체력 : ";
            selectedItemStatName[1].gameObject.SetActive(false);

            selectedItemStatValue[0].text = selectedItem.item.Hp.ToString();
            selectedItemStatValue[1].gameObject.SetActive(false);
        }

        if(selectedItem.item.Type == ItemType.Shield) // 방패 = 방어력
        {
            selectedItemName.gameObject.SetActive(true);
            selectedItemDescription.gameObject.SetActive(true);

            selectedItemStatName[0].text = "방어력 : ";
            selectedItemStatName[1].gameObject.SetActive(false);

            selectedItemStatValue[0].text = selectedItem.item.Def.ToString();
            selectedItemStatValue[1].gameObject.SetActive(false);
        }

        if(selectedItem.item.Type == ItemType.Boots) // 신발 = 스태미나 + 이동속도
        {
            selectedItemName.gameObject.SetActive(true);
            selectedItemDescription.gameObject.SetActive(true);

            selectedItemStatName[0].text = "이동속도 : ";
            selectedItemStatName[1].text = "스태미나 : ";

            selectedItemStatValue[0].text = selectedItem.item.Speed.ToString();
            selectedItemStatValue[1].text = selectedItem.item.Stamina.ToString();
        }
       

        equipButton.SetActive(selectedItem.item.Type != ItemType.Consume && !uiSlots[index].equipped);
    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;


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

        curEquipIndex = selectedItemIndex;


        Managers.DataManager.EquipItem(selectedItem.item);
        uiSlots[selectedItemIndex].icon.gameObject.SetActive(false);
        slots[selectedItemIndex].item = null;

        UpdateEquipSlots();
        UpdateItemUI();


        //UpdateUI();

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

    private void UpdateItemUI()
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

        for (int i = 0; i <  slots.Length; i++)
        {
            if (i < Managers.DataManager.playerInventoryItemData.Count &&
               Managers.DataManager.playerInventoryItemData[i].Sprite != uiSlots[i].icon.sprite)

                AddItem(Managers.DataManager.playerInventoryItemData[i]);
        }
    }

    private void UpdateEquipSlots()
    {
        for(int i = 0; i < equippedSlots.Length; i++)
        {
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Weapon))
            {
                equippedSlots[0].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Weapon].Sprite;
            }
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Armor))
            {
                equippedSlots[1].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Armor].Sprite;
            }
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Shield))
            {
                equippedSlots[2].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Shield].Sprite;
            }
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Boots))
            {
                equippedSlots[3].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Boots].Sprite;
            }
        }

    }

}
