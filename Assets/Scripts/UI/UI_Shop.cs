using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Base<UI_Shop>
{
    [Header("бс ItemData")]
    [SerializeField] private ItemSlot[] shopItemData;
    [SerializeField] private ItemSlot[] inventoryItemData;

    [Header("бс Slot")]
    [SerializeField] private ItemSlotUI[] shopSlotUI;
    [SerializeField] private ItemSlotUI[] inventorySlotUI;

    [Header("бс Item Info")]
    [SerializeField] private Text[] shopItemNameText;
    [SerializeField] private Text[] shopItemPriceText;

    [Header("бс Player Gold")]
    [SerializeField] private Text playerGoldText;

    [Header("бс PopUp UI")]
    [SerializeField] private GameObject buyPopUpUI;
    [SerializeField] private GameObject sellPopUpUI;
    [SerializeField] private GameObject cantBuyPopUpUI;
    [SerializeField] private GameObject inventoryFullPopUpUI;

    [Header("бс PopUPUI Text")]
    [SerializeField] private Text noItemSellText;
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
        Managers.SoundManager.Play("Effect/StoreBell", Sound.Effect);
        UpdateInventory();
        PopUpUIClear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI_Manager.IsActive<UI_Shop>())
            {
                CloseUI();
            }
        }
    }

    public void OnDisable()
    {
        if (Managers.UI_Manager.IsActive<UI_ItemToolTip>())
        {
            Managers.UI_Manager.HideUI<UI_ItemToolTip>();
        }
    }

    private void UpdateStore()
    {
        int itemID = 0;

        for(int i = 0; i < shopItemData.Length; i++)
        {
            itemID = DataBase.Item.GetRandomItemID();
            shopItemData[i].item = DataBase.Item.GetID(itemID);
            shopSlotUI[i].Set(shopItemData[i]);
            shopSlotUI[i].icon.sprite = shopItemData[i].item.Sprite;

            if (shopSlotUI[i].icon.sprite != null)
                shopSlotUI[i].icon.gameObject.SetActive(true);
        }
    }

    private void UpdateInventory()
    {
        playerGoldText.text = Managers.UserData.playerGold.ToString();

        for(int i = 0; i < inventoryItemData.Length; i++)
        {

            if (i < Managers.UserData.playerInventoryItemData.Count)
            {
                inventoryItemData[i].item = Managers.UserData.playerInventoryItemData[i];
                inventorySlotUI[i].Set(inventoryItemData[i]);
                inventorySlotUI[i].icon.sprite = inventoryItemData[i].item.Sprite;
            }

            if (inventorySlotUI[i].icon.sprite != null)
                inventorySlotUI[i].icon.gameObject.SetActive(true);

            if(i >= Managers.UserData.playerInventoryItemData.Count)
            {
                inventoryItemData[i].item = null;
                inventorySlotUI[i].icon.sprite = null;
                inventorySlotUI[i].icon.gameObject.SetActive(false);
            }
        }

        if (inventoryItemData[0].item == null)
        {
            noItemSellText.gameObject.SetActive(true);
        }
        else
        {
            noItemSellText.gameObject.SetActive(false);
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

    public void OnClickSellButton()
    {
        sellGoldText.text = inventoryItemData[Managers.UserData.inventoryIndex].item.SellPrice.ToString();
        sellPopUpUI.SetActive(true);
   
    }

    public void OnClickBuyButton()
    {
        buyGoldText.text = shopItemData[Managers.UserData.shopIndex].item.BuyPrice.ToString();
        buyPopUpUI.SetActive(true);
      
    }

    public void OnClickSellYesutton()
    {
        Managers.SoundManager.Play("Effect/buyitem", Sound.Effect);
        Managers.UserData.playerGold += inventoryItemData[Managers.UserData.inventoryIndex].item.SellPrice;
        Managers.UserData.playerInventoryItemData.Remove(inventoryItemData[Managers.UserData.inventoryIndex].item);
      

        sellPopUpUI.SetActive(false);
        UpdateInventory();
    }

    public void OnClickBuyYesButton()
    {

        if (shopItemData[Managers.UserData.shopIndex].item.BuyPrice <= Managers.UserData.playerGold &&
            Managers.UserData.playerInventoryItemData.Count < 28)
        {
            Managers.SoundManager.Play("Effect/buyitem", Sound.Effect);
            Managers.UserData.playerGold -= shopItemData[Managers.UserData.shopIndex].item.BuyPrice;
            Managers.UserData.AddItem(shopItemData[Managers.UserData.shopIndex].item);
          
            buyPopUpUI.SetActive(false);
            UpdateInventory();
        }
        else
        {
            buyPopUpUI.SetActive(false);
            cantBuyPopUpUI.SetActive(true);
        }

    }

    public void OnClickNoButton()
    {
        if(buyPopUpUI.activeSelf == true)
        {
            buyPopUpUI.SetActive(false);
        }
        else if (sellPopUpUI.activeSelf == true)
        {
            sellPopUpUI.SetActive(false);
        }
    }

    public void OnClickOKButton()
    {
        cantBuyPopUpUI.SetActive(false);
    }

    private void PopUpUIClear()
    {
        sellPopUpUI.SetActive(false);
        buyPopUpUI.SetActive(false);
        cantBuyPopUpUI.SetActive(false);
        inventoryFullPopUpUI.SetActive(false);
    }
}
