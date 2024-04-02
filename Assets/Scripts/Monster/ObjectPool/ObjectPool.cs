using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject poolingObjectPrefabArrow;
    [SerializeField] private GameObject poolingObjectPrefabEnergy;

    Queue<Arrow> poolingObjectQueueArrow = new Queue<Arrow>();
    Queue<Energy> poolingObjectQueueEnergy = new Queue<Energy>();

    private void Awake()
    {
        Instance = this;

        Initialize(50);
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueueArrow.Enqueue(CreateNewObjectArrow());
        }
        for (int i = 0; i < initCount; i++)
        {
            poolingObjectQueueEnergy.Enqueue(CreateNewObjectEnergy());
        }
    }

    private Arrow CreateNewObjectArrow()
    {
        var newObj = Instantiate(poolingObjectPrefabArrow, transform).GetComponent<Arrow>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private Energy CreateNewObjectEnergy()
    {
        var newObj = Instantiate(poolingObjectPrefabEnergy, transform).GetComponent<Energy>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public static Arrow GetObjectArrow()
    {
        if (Instance.poolingObjectQueueArrow.Count > 0)
        {
            var obj = Instance.poolingObjectQueueArrow.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObjectArrow();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }
    
    public static Energy GetObjectEnergy()
    {
        if (Instance.poolingObjectQueueEnergy.Count > 0)
        {
            var obj = Instance.poolingObjectQueueEnergy.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObjectEnergy();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }

    public static void ReturnObjectArrow(Arrow obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueArrow.Enqueue(obj);
    }

    public static void ReturnObjectEnergy(Energy obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueEnergy.Enqueue(obj);
    }
}
