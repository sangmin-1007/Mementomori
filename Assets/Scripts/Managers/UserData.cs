using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public List<ItemData> playerInventoryItemData = new List<ItemData>();
    public Dictionary<ItemType,ItemData> playerEquipItemDatas = new Dictionary<ItemType,ItemData>();
    public List<ItemData> storageItemData = new List<ItemData>();

    public List<ItemData> playerItemAcquired = new List<ItemData>();

    public float Master_VOLUME_KEY;
    public float BGM_VOLUME_KEY;
    public float Effect_VOLUME_KEY;



    public int playerGold;
    public int playerDeathCount;

    public int acquisitionGold = 0;
    public int inventoryIndex;
    public int equipItemIndex;
    public int storageIndex;
    public int shopIndex;
    public SlotType selectSlotType;

    public bool isTutorial = false;

    private void Awake()
    {
        Master_VOLUME_KEY = PlayerPrefs.HasKey("Master") ? PlayerPrefs.GetFloat("Master") : 1;
        BGM_VOLUME_KEY = PlayerPrefs.HasKey("BGM") ? PlayerPrefs.GetFloat("BGM") : 1;
        Effect_VOLUME_KEY = PlayerPrefs.HasKey("Effect") ? PlayerPrefs.GetFloat("Effect") : 1;
    }

    private void Start()
    {
        Managers.SoundManager.ChangeVolume();
    }
    public void AddItem(ItemData itemDatas)
    {
        if(playerInventoryItemData.Count >= 29)
            return;

        playerInventoryItemData.Add(itemDatas);
        if(Managers.UI_Manager.IsActive<UI_Inventory>())
        {
            UI_Inventory.instance.AddItem(itemDatas);
        }
    }
    public void EquipItem(ItemData itemDatas)
    {
        if (playerEquipItemDatas.ContainsKey(itemDatas.Type))
        {
            playerInventoryItemData.Add(playerEquipItemDatas[itemDatas.Type]);
            playerEquipItemDatas.Remove(itemDatas.Type);
            playerEquipItemDatas.Add(itemDatas.Type,itemDatas);
            playerInventoryItemData.Remove(itemDatas);
            return;
        }

        playerInventoryItemData.Remove(itemDatas);
        playerEquipItemDatas.Add(itemDatas.Type,itemDatas);
    }

    public void UnEquipItem(ItemData itemDatas)
    {
        playerEquipItemDatas.Remove(itemDatas.Type);
        playerInventoryItemData.Add(itemDatas);
    }

    public void StorageKeepItemData(ItemData itemDatas)
    {
        if(storageItemData.Count >= 29)
            return;

        storageItemData.Add(itemDatas);
        playerInventoryItemData.Remove(itemDatas);
    }

    public void  StorageTakeOutItemData(ItemData itemDatas)
    {
        if(playerInventoryItemData.Count >= 29)
            return;

        storageItemData.Remove(itemDatas);
        playerInventoryItemData.Add(itemDatas);
    }
}
