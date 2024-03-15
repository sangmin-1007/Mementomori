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

    //private PlayerController controller; ĳ���� InvokeEvent����̶� ȣȯ
    //private PlayerCondition condition; ���â�� �����̶� ���������� ĳ���� ����?�̶� ȣȯ�ؾ��ҵ�

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    public static Inventory instance;

    void Awake()
    {
        instance = this;
        //private PlayerController controller; ĳ���� InvokeEvent����̶� ȣȯ
        //private PlayerCondition condition; ���â�� �����̶� ���������� ĳ���� ����?�̶� ȣȯ�ؾ��ҵ�

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
            //controller.ToggleCursor(false); ����Ű�� Ŭ���ϰ��
        }
        else
        {
            inventoryWindow.SetActive(true);
            onOpenInventory?.Invoke();
            //controller.ToggleCursor(true); ����Ű�� Ŭ���ϰ��
        }
    }


    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {

    }

    void ThrowItem(ItemData item) // ������ ���������
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


    //============��ư=======================

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
