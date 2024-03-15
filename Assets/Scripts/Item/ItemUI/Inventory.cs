using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;


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
        slots = new ItemSlot[uiSlots.Length];

        for (int i = 0; i < uiSlots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        ClearSeletecItemWindow();
    }

    //�Ʒ� �κ� �÷��̾����� �����ʿ�����
    //void OnCollisionEnter2D(Collision2D coll)
    //{

    //    if (coll.gameObject.layer == LayerMask.NameToLayer("interactable")) // ���̾ "interactable"�� �����۰� �浹���� ���
    //    {
    //        curInteractGameobject = coll.gameObject;
    //        curInteractable = curInteractGameobject.GetComponent<IInteractable>();
    //        Inventory.instance.AddItem(item);
    //    }

    

    //public void OnInventory(InputValue value)
    //{
    //    if (value.isPressed)
    //    {
    //        Inventory.instance.Toggle();
    //    }
    //}

   // ======================

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

    public void AddItem(ItemData item)  // ������ ȹ��
    {
        ItemSlot emptySlot = GetEmptySlot(); // �󽽷��� ����
       
        if (emptySlot != null) // ���� ����ٸ�
        {
            emptySlot.item = item; // �ű⿡ ������ �߰�
            UpdateUI(); // UI������Ʈ �ѹ� ���ֱ�
            return;
        }
    }

    //void ThrowItem(ItemData item) ������ ���������
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
                uiSlots[i].Set(slots[i]);
            else
                uiSlots[i].Clear();
        }
    }

    ItemSlot GetEmptySlot() //â�� �ñ涧 �ʿ��ҵ�,�ȼ������� �����̳�?
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

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        
        //��.. �ڵ尡 �Ⱥҷ�����
        //for (int i = 0; i < selectedItem.item.Type; i++) 
        //{
        //    selectedItemStatName.text += selectedItem.item.consumables[i].type.ToString() + "\n";
        //    selectedItemStatValue.text += selectedItem.item.consumables[i].value.ToString() + "\n";
        //}

        equipButton.SetActive(selectedItem.item.Type == ItemType.Weapon && !uiSlots[index].equipped);
        equipButton.SetActive(selectedItem.item.Type == ItemType.Boots && !uiSlots[index].equipped);
        equipButton.SetActive(selectedItem.item.Type == ItemType.Shield && !uiSlots[index].equipped);
        equipButton.SetActive(selectedItem.item.Type == ItemType.Armor && !uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.Type == ItemType.Weapon && uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.Type == ItemType.Boots && uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.Type == ItemType.Shield && uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.Type == ItemType.Armor && uiSlots[index].equipped);
       
    }

    private void ClearSeletecItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

      
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
      
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
