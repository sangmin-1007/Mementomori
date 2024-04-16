using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectPool : MonoBehaviour
{
    Queue<GameObject> itemPool = new Queue<GameObject>();

    private const int itemCount = 50;

    private GameObject itemPoolObject;
    private GameObject itemObject;

    private void Start()
    {
        itemObject = Resources.Load<GameObject>("Prefabs/Item/ItemObject");
    }

    public void Init()
    {
        itemPool.Clear();

        itemPoolObject = GameObject.Find("itemPoolObject");

        if(itemPoolObject == null)
        {
            itemPoolObject = new GameObject("itemPoolObject");
        }


        InstantiateItem();
    }

    public void InstantiateItem()
    {
        for(int i = 0; i < itemCount; i++)
        {

            GameObject itemObjectCopy = Instantiate(itemObject);

            itemObjectCopy.transform.parent = itemPoolObject.transform;
            itemPool.Enqueue(itemObjectCopy);

            itemObjectCopy.SetActive(false);
        }
    }

    public GameObject SpawnItem(Vector3 spawnPos,int itemID)
    {
        if(itemPool.Count <= 0)
        {
            InstantiateItem();
        }

        GameObject item = itemPool.Dequeue();

        
        ItemObject objectInfo = item.GetComponent<ItemObject>();
        objectInfo.itemId = itemID;
        item.transform.position = spawnPos;
        item.SetActive(true);

        return item;
    }

    public void DisableItem(GameObject item)
    {
        item.SetActive(false);
        itemPool.Enqueue(item);
    }
}
