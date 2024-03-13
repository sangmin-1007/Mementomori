using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ItemSlot 
{
    public ItemData item;
}
public class Inventory : UI_Base<Inventory>
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject inventoryWindow;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public Text selectedItemName;
    public Text selectedItemDescription;
    public Text selectedItemStatName;
    public Text selectedItemStatValue;
    public GameObject equipButton;
    public GameObject unequipButton;

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
            UpdateUI(); // UI업데이트 한번 해주기
            return;
        }
    }

    //void ThrowItem(ItemData item) 버리기 기능있을때
    //{

    //}

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetEmptySlot() //창고 맡길때 필요할듯,팔수있으면 상점이나?
    {
        return null;
    }

    public void SelectItem(int index)
    {

    }

    private void ClearSeletecItemWindow()
    {

    }


    //============버튼=======================

    public void OnEquipButton()
    {

    }

    void UnEquip(int index)
    {

    }

    public void OnUnequipButton()
    {

    }

    public bool HasItems(ItemData item)
    {
        return false;
    }
}
