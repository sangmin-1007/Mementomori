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

    public int playerGold = 10000;
    public int playerDeathCount;

    public int acquisitionGold = 0;
    public int inventoryIndex;
    public int equipItemIndex;
    public int storageIndex;
    public int shopIndex;
    public SlotType selectSlotType;

    public bool isTutorial = false;
    public void AddItem(ItemData itemDatas)
    {
        if(playerInventoryItemData.Count >= 29)
        {
            Debug.Log("°¡µæ Ã¡½À´Ï´Ù");
            return;
        }

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
        {
            Debug.Log("°¡µæ Ã¡½À´Ï´Ù");
            return;
        }

        storageItemData.Add(itemDatas);
        playerInventoryItemData.Remove(itemDatas);
    }

    public void  StorageTakeOutItemData(ItemData itemDatas)
    {
        if(playerInventoryItemData.Count >= 29)
        {
            Debug.Log("°¡µæ Ã¡½À´Ï´Ù");
            return;
        }

        storageItemData.Remove(itemDatas);
        playerInventoryItemData.Add(itemDatas);
    }
}
