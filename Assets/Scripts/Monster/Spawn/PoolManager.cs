using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] skillPrefabs;

    List<GameObject>[] pools;
    List<GameObject>[] skillPools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

        skillPools = new List<GameObject>[skillPrefabs.Length];

        for(int i =0; i < skillPrefabs.Length; i++)
        {
            skillPools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

    public GameObject SkillGet(int index)
    {
        GameObject select = null;

        foreach (GameObject item in skillPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(skillPrefabs[index], transform);
            skillPools[index].Add(select);
        }

        return select;
    }
}
