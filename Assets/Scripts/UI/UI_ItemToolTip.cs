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

    private float xPos;
    private RectTransform rt;

    public override void OnEnable()
    {
        base.OnEnable();
        InputKeyText();
    }

    private void Start()
    {

        xPos = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rt = toolTip.GetComponent<RectTransform>();
    }

    private void Update()
    {
        toolTip.transform.position = Input.mousePosition;

        if (rt.anchoredPosition.x + rt.sizeDelta.x > xPos && rt.anchoredPosition.y + rt.sizeDelta.y > -70f)
            rt.pivot = new Vector2(1, 1);
        else if (rt.anchoredPosition.x + rt.sizeDelta.x < xPos && rt.anchoredPosition.y + rt.sizeDelta.y > -70f)
            rt.pivot = new Vector2(0, 1);
        else if (rt.anchoredPosition.x + rt.sizeDelta.x > xPos && rt.anchoredPosition.y + rt.sizeDelta.y < -70f)
            rt.pivot = new Vector2(1, 0);
        else if (rt.anchoredPosition.y + rt.sizeDelta.y < -70f)
            rt.pivot = new Vector2(0, 0);
    }

    private void InputKeyText()
    {
        selectedSlotType = Managers.UserData.selectSlotType;

        switch (selectedSlotType)
        {
            case SlotType.Inventory:
                inputKeyText.text = "우클릭 - 장착";
                break;
            case SlotType.Storage:
                inputKeyText.text = "우클릭 - 꺼내기";
                break;
            case SlotType.Storage_Inventory:
                inputKeyText.text = "우클릭 - 보관";
                break;
            case SlotType.Shop:
                inputKeyText.text = "우클릭 - 구매하기";
                break;
            case SlotType.Shop_Inventory:
                inputKeyText.text = "우클릭 - 판매하기";
                break;
            case SlotType.Equip:
                inputKeyText.text = "우클릭 - 해제";
                break;
        }
    }

    public void ItemInfoText(ItemData itemData)
    {
        ItemNameText.text = itemData.Name;
        descriptionText.text = itemData.Description;

        ItemNameText.color = Colors.ItemGrade[(int)itemData.Grade];
        outLine.effectColor = Colors.ItemGrade[(int)itemData.Grade];

        switch (itemData.Type)
        {
            case ItemType.Weapon:
                selectedItemStat[0].text = "공격력 : ";
                selectedItemStatValue[0].text = itemData.Atk.ToString();
                selectedItemStat[1].text = "공격속도 : ";
                selectedItemStatValue[1].text = itemData.AtkSpeed.ToString();
                break;
            case ItemType.Armor:
                selectedItemStat[0].text = "체력 : ";
                selectedItemStatValue[0].text = itemData.Hp.ToString();
                selectedItemStat[1].text = "";
                selectedItemStatValue[1].text = "";
                break;
            case ItemType.Shield:
                selectedItemStat[0].text = "방어력 : ";
                selectedItemStatValue[0].text = itemData.Def.ToString();
                selectedItemStat[1].text = "";
                selectedItemStatValue[1].text = "";
                break;
            case ItemType.Boots:
                selectedItemStat[0].text = "이동속도 : ";
                selectedItemStatValue[0].text = itemData.Speed.ToString();
                selectedItemStat[1].text = "스태미나 : ";
                selectedItemStatValue[1].text = itemData.Stamina.ToString();
                break;
        }

    }
}
