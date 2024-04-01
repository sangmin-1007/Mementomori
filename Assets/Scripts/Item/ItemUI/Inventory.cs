using Constants;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class ItemSlot 
{
    public ItemData item;
}
public class Inventory : UI_Base<Inventory>
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public ItemSlotUI[] equippedSlots;
    public ItemSlot[] equipSlots;


    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private ItemSlot equipSelectedItem;
    private int selectedItemIndex;
    private int equipSelectedItemIndex;
    
    public Text selectedItemName;
    public Text selectedItemDescription;

    public Text[] selectedItemStatName;
    public Text[] selectedItemStatValue;

    public GameObject ToolTip;
    
    private int curEquipIndex;
    private Outline _outline;


    public static Inventory instance;

    void Awake()
    {
        instance = this;

        _outline = ToolTip.GetComponentInChildren<Outline>();

        slots = new ItemSlot[uiSlots.Length];
        equipSlots = new ItemSlot[equippedSlots.Length];

        for (int i = 0; i < uiSlots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            equipSlots[i] = new ItemSlot();
            equippedSlots[i].index = i;
            equippedSlots[i].Clear();
        }

        ClearSeletecItemWindow();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        UpdateUI();
        UpdateEquipSlots();
        UpdateItemUI();
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

        for (int i = 0; i < equipSlots.Length; i++)
        {

            if (equipSlots[i].item != null) 
            {
                equippedSlots[i].Set(equipSlots[i]);

            }
            else
                equippedSlots[i].Clear();
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

    public void SelectItem(int index,Vector3 _pos)
    {
      
            ToolTip.SetActive(true);
            _pos += new Vector3(ToolTip.GetComponent<RectTransform>().rect.width * 0.5f,
                -ToolTip.GetComponent<RectTransform>().rect.height * 0.5f);
            ToolTip.transform.position = _pos;


        if (slots[index].item == null)
        {

            ToolTip.SetActive(false);
        }
      

    

            selectedItem = slots[index];
            selectedItemIndex = index;

            Color Color = Colors.ItemGrade[(int)selectedItem.item.Grade];
             selectedItemName.color = Color;
             _outline.effectColor = Color;


            selectedItemName.text = selectedItem.item.Name;
            selectedItemDescription.text = selectedItem.item.Description;
            



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

            if (selectedItem.item.Type == ItemType.Armor) // 갑옷 = 체력
            {
                selectedItemName.gameObject.SetActive(true);
                selectedItemDescription.gameObject.SetActive(true);

                selectedItemStatName[0].text = "체력 : ";
                selectedItemStatName[1].gameObject.SetActive(false);

                selectedItemStatValue[0].text = selectedItem.item.Hp.ToString();
                selectedItemStatValue[1].gameObject.SetActive(false);
            }

            if (selectedItem.item.Type == ItemType.Shield) // 방패 = 방어력
            {
                selectedItemName.gameObject.SetActive(true);
                selectedItemDescription.gameObject.SetActive(true);

                selectedItemStatName[0].text = "방어력 : ";
                selectedItemStatName[1].gameObject.SetActive(false);

                selectedItemStatValue[0].text = selectedItem.item.Def.ToString();
                selectedItemStatValue[1].gameObject.SetActive(false);
            }

            if (selectedItem.item.Type == ItemType.Boots) // 신발 = 스태미나 + 이동속도
            {
                selectedItemName.gameObject.SetActive(true);
                selectedItemDescription.gameObject.SetActive(true);

                selectedItemStatName[0].text = "이동속도 : ";
                selectedItemStatName[1].text = "스태미나 : ";

                selectedItemStatValue[0].text = selectedItem.item.Speed.ToString();
                selectedItemStatValue[1].text = selectedItem.item.Stamina.ToString();
            }
        }

    public void SelectEquipItem(int index, Vector3 _pos)
    {
        ToolTip.SetActive(true);
        _pos += new Vector3(ToolTip.GetComponent<RectTransform>().rect.width * 0.5f,
            -ToolTip.GetComponent<RectTransform>().rect.height * 0.5f);
        ToolTip.transform.position = _pos;


        if (equipSlots[index].item == null)
        {
            ToolTip.SetActive(false);
        }

        selectedItem = equipSlots[index];
        selectedItemIndex = index;



        equipSelectedItem = equipSlots[index];
        equipSelectedItemIndex = index;

        if (equipSlots[selectedItemIndex] != null)
        {
            Color GradeColorEquipSelected = Colors.ItemGrade[(int)equipSelectedItem.item.Grade];

            selectedItemName.color = GradeColorEquipSelected;
            _outline.effectColor = GradeColorEquipSelected;
        }
 

        selectedItemName.text = equipSelectedItem.item.Name;

        selectedItemDescription.text = equipSelectedItem.item.Description;

        for (int i = 0; i < selectedItemStatName.Length; i++)
        {
            selectedItemStatName[i].gameObject.SetActive(true);
            selectedItemStatValue[i].gameObject.SetActive(true);
        }


        if (equipSelectedItem.item.Type == ItemType.Weapon) // 아이템이 검일때 검은 공격력/공격속도만 보여준다.
        {

            selectedItemStatName[0].text = "공격력 : ";
            selectedItemStatName[1].text = "공격속도 :";

            selectedItemStatValue[0].text = equipSelectedItem.item.Atk.ToString();
            selectedItemStatValue[1].text = equipSelectedItem.item.AtkSpeed.ToString();


        }

        if (equipSelectedItem.item.Type == ItemType.Armor) // 갑옷 = 체력
        {
            selectedItemName.gameObject.SetActive(true);
            selectedItemDescription.gameObject.SetActive(true);

            selectedItemStatName[0].text = "체력 : ";
            selectedItemStatName[1].gameObject.SetActive(false);

            selectedItemStatValue[0].text = equipSelectedItem.item.Hp.ToString();
            selectedItemStatValue[1].gameObject.SetActive(false);
        }

        if (equipSelectedItem.item.Type == ItemType.Shield) // 방패 = 방어력
        {
            selectedItemName.gameObject.SetActive(true);
            selectedItemDescription.gameObject.SetActive(true);

            selectedItemStatName[0].text = "방어력 : ";
            selectedItemStatName[1].gameObject.SetActive(false);

            selectedItemStatValue[0].text = equipSelectedItem.item.Def.ToString();
            selectedItemStatValue[1].gameObject.SetActive(false);
        }

        if (equipSelectedItem.item.Type == ItemType.Boots) // 신발 = 스태미나 + 이동속도
        {
            selectedItemName.gameObject.SetActive(true);
            selectedItemDescription.gameObject.SetActive(true);

            selectedItemStatName[0].text = "이동속도 : ";
            selectedItemStatName[1].text = "스태미나 : ";

            selectedItemStatValue[0].text = equipSelectedItem.item.Speed.ToString();
            selectedItemStatValue[1].text = equipSelectedItem.item.Stamina.ToString();
        }
    }

    public void ClearSeletecItemWindow()
    {
        ToolTip.SetActive(false);
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
    }


    //============버튼=======================

    public void OnEquipButton()
    {
        //selectedItem = ;
        //Managers.DataManager.EquipItem(selectedItem.item);
        Managers.DataManager.EquipItem(Managers.DataManager.playerInventoryItemData[Managers.DataManager.inventoryIndex]);
        uiSlots[selectedItemIndex].icon.gameObject.SetActive(false);
        slots[selectedItemIndex].item = null;

        UpdateEquipSlots();
        UpdateUI();
        UpdateItemUI();
    }

    public void UnEquip()
    {

        Managers.DataManager.UnEquipItem(equipSlots[Managers.DataManager.equipItemIndex].item);

        equippedSlots[Managers.DataManager.equipItemIndex].icon.gameObject.SetActive(false);
        equipSlots[Managers.DataManager.equipItemIndex].item = null;

        UpdateEquipSlots();
        UpdateUI();
        UpdateItemUI();
    }

    public void UpdateItemUI()
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

        for (int i = 0; i < slots.Length; i++)
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
            equippedSlots[i].Clear();
        }

        for (int i = 0; i < equippedSlots.Length; i++)
        {

            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Weapon))
            {
                equippedSlots[0].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Weapon].Sprite;
                equipSlots[0].item = Managers.DataManager.playerEquipItemDatas[ItemType.Weapon];
            }
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Armor))
            {
                equippedSlots[1].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Armor].Sprite;
                equipSlots[1].item = Managers.DataManager.playerEquipItemDatas[ItemType.Armor];
            }
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Shield))
            {
                equippedSlots[2].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Shield].Sprite;
                equipSlots[2].item = Managers.DataManager.playerEquipItemDatas[ItemType.Shield];
            }
            if (Managers.DataManager.playerEquipItemDatas.ContainsKey(ItemType.Boots))
            {
                equippedSlots[3].icon.sprite = Managers.DataManager.playerEquipItemDatas[ItemType.Boots].Sprite;
                equipSlots[3].item = Managers.DataManager.playerEquipItemDatas[ItemType.Boots];
            }
            
        }
    }

}
