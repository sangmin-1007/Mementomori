using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ItemSlot
{
    public ItemData item;
}
public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uislots;
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
        slots = new ItemSlot[uislots.Length];

        for (int i = 0; i < uislots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uislots[i].index = i;
            uislots[i].Clear();
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

    public void AddItem(ItemData item)
    {

    }

    void ThrowItem(ItemData item) // 버리기 기능있을때
    {

    }

    void UpdateUI()
    {

    }

    ItemSlot GetEmptySlot()
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
