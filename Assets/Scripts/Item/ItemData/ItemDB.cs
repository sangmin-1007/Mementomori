using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB 
{
    private Dictionary<int, ItemData> _items = new();
    // int에는 _id키값(10001001)를 넣어준다.

    public ItemDB() //_items에 SO를 넣어주는 코드 
    {
        var res = Resources.Load<ItemDBSheet>("DB/ItemDBSheet"); // item Scriptable Object를 불러옴
        var itemsSo = UnityEngine.Object.Instantiate(res);               // 로드를 해주고 instantiate로 생성
        var entites = itemsSo.Entities;                      // Entities에 접근을 해줌, (Entities에는 Scriptable Object안에 내용들이 있다)

        if (entites == null || entites.Count <= 0)
            return;

        var entityCount = entites.Count; //Length로도 사용하지만 데이터가 많기때문에 Count로 사용
        for (int i = 0; i < entityCount; i++)  //for문으로 돌리면서 SO에 있는 데이터를 Dictionary에 저장해둔다.
        {
            var item = entites[i];

            if (_items.ContainsKey(item.Id))
                _items[item.Id] = item;
            else
                _items.Add(item.Id, item);

        }


    }
    public ItemData GetID(int id) // id를 통해 원하는 데이터로 접근하는 함수
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

    public IEnumerator DbEnumerator() //  사용함 하나의 키값으로 아이템을 얻는게아니라, 전체아이템을 확인하고싶을떄 사용함
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
