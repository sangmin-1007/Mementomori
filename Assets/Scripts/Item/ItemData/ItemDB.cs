using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    private Dictionary<int, ItemData> _items = new();

    public ItemDB()
    {
        var res = Resources.Load<ItemDBSheet>("ItemDBSheet");
        var itemsSo = Object.Instantiate(res);
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
    public ItemData Get(int id)
    {
        if(_items.ContainsKey(id))
            return _items[id];

        return null;
    }

    public IEnumerator DbEnumerator()
    {
        return _items.GetEnumerator();
    }
}
