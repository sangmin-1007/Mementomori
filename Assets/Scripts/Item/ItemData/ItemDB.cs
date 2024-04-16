using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB 
{
    private Dictionary<int, ItemData> _items = new();


    public ItemDB()
    {
        var res = Resources.Load<ItemDBSheet>("DB/ItemDBSheet");
        var itemsSo = UnityEngine.Object.Instantiate(res);               
        var entites = itemsSo.Entities;                     

        if (entites == null || entites.Count <= 0)
            return;

        var entityCount = entites.Count; 
        for (int i = 0; i < entityCount; i++)
        {
            var item = entites[i];

            if (_items.ContainsKey(item.Id))
                _items[item.Id] = item;
            else
                _items.Add(item.Id, item);

        }


    }
    public ItemData GetID(int id) 
    {
        if(_items.ContainsKey(id))
            return _items[id];

        return null;
    }

    public int GetRandomItemID()
    {

        int randomType = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ItemType)).Length - 1);
        int randomGrade = UnityEngine.Random.Range(1, 101);
        int randomIndex = UnityEngine.Random.Range(0, 3);
        ItemGrade itemGrade = RandomItemGrade(randomGrade);

        foreach (var item in _items)
        {
            if(item.Value.Type == (ItemType)randomType && item.Value.Grade == itemGrade)
            {
                return item.Key + randomIndex;
            }
        }

        return 0;
    }

    public IEnumerator DbEnumerator() 
    {
        return _items.GetEnumerator();
    }

    private ItemGrade RandomItemGrade(int randomIndex)
    {


        if(randomIndex <= 70)
        {
            return ItemGrade.Normal;
        }
        else if(randomIndex > 70 && 85 <= randomIndex)
        {
            return ItemGrade.Rare;
        }
        else if(randomIndex > 85 && randomIndex <= 95)
        {
            return ItemGrade.Unique;
        }
        else
        {
            return ItemGrade.Legend;
        }

    }
}
