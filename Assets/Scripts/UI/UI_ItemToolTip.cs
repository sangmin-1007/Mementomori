using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemToolTip : UI_Base<UI_ItemToolTip>
{

    [SerializeField] private Text ItemNameText;
    [SerializeField] private Text discriptionText;

    [SerializeField] private Text[] selectedItemStatType;
    [SerializeField] private Text[] selectedItemStatValue;
    //[SerializeField] private 

    private ItemData selectedItem;
    private int selectedItemIndex;


    //public void SelectItem(int index, Vector3 _pos) 
    //{


    //    _pos += new Vector3(this.GetComponent<RectTransform>().rect.width * 0.5f,
    //        -this.GetComponent<RectTransform>().rect.height * 0.5f);

    //    this.transform.position = _pos;


    //    if (index >= Managers.DataManager.playerInventoryItemData.Count)
    //    {

    //        Managers.UI_Manager.HideUI<UI_ItemToolTip>();
    //        return;
    //    }




    //    selectedItem = Managers.DataManager.playerInventoryItemData[index];
    //    selectedItemIndex = index;

    //    Color Color = Colors.ItemGrade[(int)selectedItem.item.Grade];
    //    ItemNameText.color = Color;
    //    _outline.effectColor = Color;


    //    selectedItemName.text = selectedItem.item.Name;
    //    selectedItemDescription.text = selectedItem.item.Description;




    //    for (int i = 0; i < selectedItemStatType.Length; i++)
    //    {
    //        selectedItemStatType[i].gameObject.SetActive(true);
    //        selectedItemStatValue[i].gameObject.SetActive(true);
    //    }


    //    if (selectedItem.item.Type == ItemType.Weapon) // 아이템이 검일때 검은 공격력/공격속도만 보여준다.
    //    {

    //        selectedItemStatType[0].text = "공격력 : ";
    //        selectedItemStatType[1].text = "공격속도 :";

    //        selectedItemStatValue[0].text = selectedItem.item.Atk.ToString();
    //        selectedItemStatValue[1].text = selectedItem.item.AtkSpeed.ToString();
    //    }

    //    if (selectedItem.item.Type == ItemType.Armor) // 갑옷 = 체력
    //    {
    //        selectedItemName.gameObject.SetActive(true);
    //        selectedItemDescription.gameObject.SetActive(true);

    //        selectedItemStatType[0].text = "체력 : ";
    //        selectedItemStatType[1].gameObject.SetActive(false);

    //        selectedItemStatValue[0].text = selectedItem.item.Hp.ToString();
    //        selectedItemStatValue[1].gameObject.SetActive(false);
    //    }

    //    if (selectedItem.item.Type == ItemType.Shield) // 방패 = 방어력
    //    {
    //        selectedItemName.gameObject.SetActive(true);
    //        selectedItemDescription.gameObject.SetActive(true);

    //        selectedItemStatType[0].text = "방어력 : ";
    //        selectedItemStatType[1].gameObject.SetActive(false);

    //        selectedItemStatValue[0].text = selectedItem.item.Def.ToString();
    //        selectedItemStatValue[1].gameObject.SetActive(false);
    //    }

    //    if (selectedItem.item.Type == ItemType.Boots) // 신발 = 스태미나 + 이동속도
    //    {
    //        selectedItemName.gameObject.SetActive(true);
    //        selectedItemDescription.gameObject.SetActive(true);

    //        selectedItemStatType[0].text = "이동속도 : ";
    //        selectedItemStatType[1].text = "스태미나 : ";

    //        selectedItemStatValue[0].text = selectedItem.item.Speed.ToString();
    //        selectedItemStatValue[1].text = selectedItem.item.Stamina.ToString();
    //    }
    //}
}
