using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<ItemData> playerInventoryItemData = new List<ItemData>();
    public Dictionary<ItemType,ItemData> playerEquipItemDatas = new Dictionary<ItemType,ItemData>();
    public List<ItemData> storageItemData = new List<ItemData>();

    public int playerGold;
    
    public void AddItem(ItemData itemDatas)
    {
        playerInventoryItemData.Add(itemDatas);
        if(Managers.UI_Manager.IsActive<Inventory>())
        {
            Inventory.instance.AddItem(itemDatas);
        }
    }
    public void EquipItem(ItemData itemDatas)
    {
        playerInventoryItemData.Remove(itemDatas);
        playerEquipItemDatas.Add(itemDatas.Type,itemDatas);
    }
    public void StorageItemData(ItemData itemDatas)
    {
        storageItemData.Add(itemDatas);
    }
}
