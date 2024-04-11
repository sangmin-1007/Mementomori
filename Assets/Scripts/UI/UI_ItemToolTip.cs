using Constants;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemToolTip : UI_Base<UI_ItemToolTip>
{

    [SerializeField] private Text ItemNameText;
    [SerializeField] private Text descriptionText;

    [SerializeField] private Text inputKeyText;

    [SerializeField] private Text[] selectedItemStat;
    [SerializeField] private Text[] selectedItemStatValue;

    [SerializeField] private GameObject toolTip;
    [SerializeField] private Outline outLine;

    private SlotType selectedSlotType;

    public override void OnEnable()
    {
        base.OnEnable();

        InputKeyText();
    }

    private void InputKeyText()
    {
        selectedSlotType = Managers.UserData.selectSlotType;

        switch (selectedSlotType)
        {
            case SlotType.Inventory:
                inputKeyText.text = "��Ŭ�� - ����";
                break;
            case SlotType.Storage:
                inputKeyText.text = "��Ŭ�� - ������";
                break;
            case SlotType.Storage_Inventory:
                inputKeyText.text = "��Ŭ�� - ����";
                break;
            case SlotType.Shop:
                inputKeyText.text = "��Ŭ�� - �����ϱ�";
                break;
            case SlotType.Shop_Inventory:
                inputKeyText.text = "��Ŭ�� - �Ǹ��ϱ�";
                break;
            case SlotType.Equip:
                inputKeyText.text = "��Ŭ�� - ����";
                break;
        }
    }

    public void ItemInfoText(ItemData itemData,Vector3 _pos)
    {
        ItemNameText.text = itemData.Name;
        descriptionText.text = itemData.Description;

        _pos += new Vector3(toolTip.GetComponent<RectTransform>().rect.width * 0.5f,
            -toolTip.GetComponent<RectTransform>().rect.height * 0.5f);
        toolTip.transform.position = _pos;

        ItemNameText.color = Colors.ItemGrade[(int)itemData.Grade];
        outLine.effectColor = Colors.ItemGrade[(int)itemData.Grade];

        switch (itemData.Type)
        {
            case ItemType.Weapon:
                selectedItemStat[0].text = "���ݷ� : ";
                selectedItemStatValue[0].text = itemData.Atk.ToString();
                selectedItemStat[1].text = "���ݼӵ� : ";
                selectedItemStatValue[1].text = itemData.AtkSpeed.ToString();
                break;
            case ItemType.Armor:
                selectedItemStat[0].text = "ü�� : ";
                selectedItemStatValue[0].text = itemData.Hp.ToString();
                selectedItemStat[1].text = "";
                selectedItemStatValue[1].text = "";
                break;
            case ItemType.Shield:
                selectedItemStat[0].text = "���� : ";
                selectedItemStatValue[0].text = itemData.Def.ToString();
                selectedItemStat[1].text = "";
                selectedItemStatValue[1].text = "";
                break;
            case ItemType.Boots:
                selectedItemStat[0].text = "�̵��ӵ� : ";
                selectedItemStatValue[0].text = itemData.Speed.ToString();
                selectedItemStat[1].text = "���¹̳� : ";
                selectedItemStatValue[1].text = itemData.Stamina.ToString();
                break;
        }

    }
}
