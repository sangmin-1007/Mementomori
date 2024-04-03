using System;
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
    public int playerDeathCount;
    public DateTime dateTime;
}

public class DataManager : MonoBehaviour
{
    public string path;
    public int nowSlot;

    public SaveData nowPlayerData = new SaveData();

    private void Awake()
    {
        path = Application.persistentDataPath + "/save";
        print(path);
    }

    public void Save()
    {

        nowPlayerData.playerGold = Managers.UserData.playerGold;

        for (int i = 0; i < Managers.UserData.playerInventoryItemData.Count; i++)
        {
            nowPlayerData.inventoryItemData.Add(Managers.UserData.playerInventoryItemData[i]);
        }

        foreach(ItemData item in Managers.UserData.playerEquipItemDatas.Values)
        {
            nowPlayerData.equipItemData.Add(item);
        }

        for(int i = 0; i < Managers.UserData.storageItemData.Count; i++)
        {
            nowPlayerData.storageItemData.Add(Managers.UserData.storageItemData[i]);
        }

        nowPlayerData.dateTime = DateTime.Now;
        nowPlayerData.playerDeathCount = Managers.UserData.playerDeathCount;

        string data = JsonUtility.ToJson(nowPlayerData);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void Load()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayerData = JsonUtility.FromJson<SaveData>(data);



        for (int i = 0; i < nowPlayerData.inventoryItemData.Count; i++)
        {
            Managers.UserData.playerInventoryItemData.Add(nowPlayerData.inventoryItemData[i]);
        }

        for(int i = 0; i < nowPlayerData.equipItemData.Count; i++)
        {
            Managers.UserData.playerEquipItemDatas.Add(nowPlayerData.equipItemData[i].Type, nowPlayerData.equipItemData[i]);
        }

        for (int i = 0; i < nowPlayerData.storageItemData.Count; i++)
        {
            Managers.UserData.storageItemData.Add(nowPlayerData.storageItemData[i]);
        }

        Managers.UserData.playerDeathCount = nowPlayerData.playerDeathCount;
        Managers.UserData.playerGold = nowPlayerData.playerGold;
    }
}
