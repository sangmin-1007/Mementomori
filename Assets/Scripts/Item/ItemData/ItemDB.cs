using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    private Dictionary<int, ItemData> _items = new();
    // int���� _idŰ��(10001001)�� �־��ش�.

    public ItemDB() //_items�� SO�� �־��ִ� �ڵ� 
    {
        var res = Resources.Load<ItemDBSheet>("ItemDBSheet"); // item Scriptable Object�� �ҷ���
        var itemsSo = Object.Instantiate(res);               // �ε带 ���ְ� instantiate�� ����
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
    public ItemData Get(int id) // id�� ���� ���ϴ� �����ͷ� �����ϴ� �Լ�
    {
        if(_items.ContainsKey(id))
            return _items[id];

        return null;
    }

    public IEnumerator DbEnumerator() //  ����� �ϳ��� Ű������ �������� ��°Ծƴ϶�, ��ü�������� Ȯ���ϰ������ �����
    {
        return _items.GetEnumerator();
    }
}
