using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public List<ItemData> inventoryItemData = new List<ItemData>();
    public List<ItemData> equipItemData = new List<ItemData>();
    public List<ItemData> storageItemData = new List<ItemData>();

    public int playerGold;
}

public class DataManager : MonoBehaviour
{
    string path;

    private void Start()
    {

    }

    public void Save()
    {
        SaveData saveData = new SaveData();

        saveData.playerGold = Managers.UserData.playerGold;

        for (int i = 0; i < Managers.UserData.playerInventoryItemData.Count; i++)
        {
            saveData.inventoryItemData.Add(Managers.UserData.playerInventoryItemData[i]);
        }

        foreach(ItemData item in Managers.UserData.playerEquipItemDatas.Values)
        {
            saveData.equipItemData.Add(item);
        }

        for(int i = 0; i < Managers.UserData.storageItemData.Count; i++)
        {
            saveData.storageItemData.Add(Managers.UserData.storageItemData[i]);
        }


    }

    public void Load()
    {

    }
}
