using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public List<ItemData> inventoryItemData = new List<ItemData>();
    public List<ItemData> EquipItemData = new List<ItemData>();
    public List<ItemData> storageItemData = new List<ItemData>();

    public int playerGold;
}

public class DataManager : MonoBehaviour
{
    string path;

    public void Save()
    {
        SaveData saveData = new SaveData();
    }

    public void Load()
    {

    }
}
