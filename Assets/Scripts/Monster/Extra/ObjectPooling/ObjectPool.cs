using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject pooledObject; // 처리하고자 하는 오브젝트
    public int poolCount = 10; // 탄 갯수
    public bool more = true;
    // poolCount보다 많은 오브젝트 갯수가 화면에 등장해야 할 경우
    // 추가적으로 오브젝트를 생성해주기 위한 변수
    // 즉, 화면에 보여주는 필요한 만큼만 생성할 수 있게됨

    private List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        while (poolCount > 0)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            poolCount -= 1;
        } // 오브젝트 10개 생성
    }

    public GameObject GetObject()
    {
        // Object Pool에서 하나의 오브젝트를 꺼내는 함수
        foreach (GameObject obj in pooledObjects)
        { // Pool을 뒤져서
            if (!obj.activeInHierarchy)
            { //비활성 오브젝트가 있다면
                return obj; // 반환해서 사용할 수 있도록 함
            }
        }
        if (more)
        {
            // 게임 내에 오브젝트가 전부 활성화 되었을 때
            GameObject obj = Instantiate(pooledObject);
            // 추가 오브젝트 생성
            pooledObjects.Add(obj);
            return obj;
        }
        return null; // 오류났을 때 null 반환 (메모리 공간 없을 때)
    }
    // 그래서 GetObject를 받아가는 쪽에서 활성화시켜서 사용하면 됨
}
