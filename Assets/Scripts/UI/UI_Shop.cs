using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Base<UI_Shop>
{
    [SerializeField] private ItemSlot[] shopItemData;
    [SerializeField] private ItemSlot[] inventoryItemData;

    [SerializeField] private ItemSlotUI[] shopSlotUI;
    [SerializeField] private ItemSlotUI[] inventorySlotUI;

    [SerializeField] private Text[] shopItemNameText;
    [SerializeField] private Text[] shopItemPriceText;

    [SerializeField] private Text sellGoldText;
    [SerializeField] private Text buyGoldText;

    private void Awake()
    {
        shopItemData = new ItemSlot[shopSlotUI.Length];
        inventoryItemData = new ItemSlot[inventorySlotUI.Length];

        for(int i = 0; i < shopSlotUI.Length; i++)
        {
            shopItemData[i] = new ItemSlot();
            shopSlotUI[i].index = i;
            shopSlotUI[i].icon.gameObject.SetActive(false);
        }

        for(int i = 0; i < inventorySlotUI.Length; i++)
        {
            inventoryItemData[i] = new ItemSlot();
            inventorySlotUI[i].index = i;
            inventorySlotUI[i].icon.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        UpdateStore();
        UpdateShopText();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        UpdateInventory();
    }

    private void UpdateStore()
    {
        int itemID = 0;

        for(int i = 0; i < shopItemData.Length; i++)
        {
            itemID = DataBase.Item.GetRandomItemID();
            shopItemData[i].item = DataBase.Item.GetID(itemID);
            shopSlotUI[i].icon.sprite = shopItemData[i].item.Sprite;

            if (shopSlotUI[i].icon.sprite != null)
                shopSlotUI[i].icon.gameObject.SetActive(true);
        }
    }

    private void UpdateInventory()
    {
        for(int i = 0; i < inventoryItemData.Length; i++)
        {
            if (i < Managers.DataManager.playerInventoryItemData.Count)
            {
                inventoryItemData[i].item = Managers.DataManager.playerInventoryItemData[i];
                inventorySlotUI[i].icon.sprite = inventoryItemData[i].item.Sprite;
            }

            if (inventorySlotUI[i].icon.sprite != null)
                inventorySlotUI[i].icon.gameObject.SetActive(true);
        }
    }

    private void UpdateShopText()
    {
        for(int i = 0; i < shopItemData.Length; i++)
        {
            shopItemNameText[i].text = shopItemData[i].item.Name;
            shopItemPriceText[i].text = shopItemData[i].item.BuyPrice.ToString();
        }
    }

    private void OnClickSellButton()
    {

    }

    private void OnClickBuyButton()
    {

    }

    public void OnClickSellPopUpButton()
    {

    }

    public void OnClickBuyPopUpButton()
    {

    }

    public void OnClickNoButton()
    {

    }
}
