using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public List<ItemData> playerItemData = new List<ItemData>();
    public List<ItemData> playerEquipItemData = new List<ItemData>();
    public List<ItemData> storageItemData = new List<ItemData>();

    public int playerGold;
    
    public void AddItem(ItemData itemDatas)
    {
        playerItemData.Insert(0,itemDatas);
    }
    public void EquipItem(ItemData itemDatas)
    {
        playerItemData.Remove(itemDatas);
        playerEquipItemData.Add(itemDatas);
    }
    public void StorageItemData(ItemData itemDatas)
    {
        playerItemData.Remove(itemDatas);
        playerEquipItemData.Remove(itemDatas);
    }
}
