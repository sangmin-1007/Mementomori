using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB 
{
    private Dictionary<int, ItemData> _items = new();
    // int���� _idŰ��(10001001)�� �־��ش�.

    public ItemDB() //_items�� SO�� �־��ִ� �ڵ� 
    {
        var res = Resources.Load<ItemDBSheet>("DB/ItemDBSheet"); // item Scriptable Object�� �ҷ���
        var itemsSo = UnityEngine.Object.Instantiate(res);               // �ε带 ���ְ� instantiate�� ����
        var entites = itemsSo.Entities;                      // Entities�� ������ ����, (Entities���� Scriptable Object�ȿ� ������� �ִ�)

        if (entites == null || entites.Count <= 0)
            return;

        var entityCount = entites.Count; //Length�ε� ��������� �����Ͱ� ���⶧���� Count�� ���
        for (int i = 0; i < entityCount; i++)  //for������ �����鼭 SO�� �ִ� �����͸� Dictionary�� �����صд�.
        {
            var item = entites[i];

            if (_items.ContainsKey(item.Id))
                _items[item.Id] = item;
            else
                _items.Add(item.Id, item);

        }


    }
    public ItemData GetID(int id) // id�� ���� ���ϴ� �����ͷ� �����ϴ� �Լ�
    {
        if(_items.ContainsKey(id))
            return _items[id];

        return null;
    }

    public int GetRandomItemID()
    {

        int randomType = UnityEngine.Random.Range(0,Enum.GetValues(typeof(ItemType)).Length - 1);
        int randomIndex = UnityEngine.Random.Range(1, 101);
        ItemGrade randomGrade = RandomItemGrade(randomIndex);

        foreach (var item in _items)
        {
            if(item.Value.Type == (ItemType)randomType && item.Value.Grade == randomGrade)
            {
                return item.Key;
            }
        }

        return 0;
    }

    public IEnumerator DbEnumerator() //  ����� �ϳ��� Ű������ �������� ��°Ծƴ϶�, ��ü�������� Ȯ���ϰ������ �����
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
