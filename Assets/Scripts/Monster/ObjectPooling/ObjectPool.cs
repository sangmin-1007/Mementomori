using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject pooledObject; // ó���ϰ��� �ϴ� ������Ʈ
    public int poolCount = 10; // ź ����
    public bool more = true;
    // poolCount���� ���� ������Ʈ ������ ȭ�鿡 �����ؾ� �� ���
    // �߰������� ������Ʈ�� �������ֱ� ���� ����
    // ��, ȭ�鿡 �����ִ� �ʿ��� ��ŭ�� ������ �� �ְԵ�

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
        } // ������Ʈ 10�� ����
    }

    public GameObject GetObject()
    {
        // Object Pool���� �ϳ��� ������Ʈ�� ������ �Լ�
        foreach (GameObject obj in pooledObjects)
        { // Pool�� ������
            if (!obj.activeInHierarchy)
            { //��Ȱ�� ������Ʈ�� �ִٸ�
                return obj; // ��ȯ�ؼ� ����� �� �ֵ��� ��
            }
        }
        if (more)
        {
            // ���� ���� ������Ʈ�� ���� Ȱ��ȭ �Ǿ��� ��
            GameObject obj = Instantiate(pooledObject);
            // �߰� ������Ʈ ����
            pooledObjects.Add(obj);
            return obj;
        }
        return null; // �������� �� null ��ȯ (�޸� ���� ���� ��)
    }
    // �׷��� GetObject�� �޾ư��� �ʿ��� Ȱ��ȭ���Ѽ� ����ϸ� ��
}
