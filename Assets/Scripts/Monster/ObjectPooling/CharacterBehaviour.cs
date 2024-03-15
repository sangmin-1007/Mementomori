using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public Transform firePos;

    public GameObject bulletObjectPool;
    private ObjectPool bulletObjectPooler;

    void Start()
    {
        bulletObjectPooler = bulletObjectPool.GetComponent<ObjectPool>();
        StartCoroutine(Fire(0.1f));
    }

    private IEnumerator Fire(float coolTime)
    {
        while (true)
        {
            GameObject bullet = bulletObjectPooler.GetObject();
            // ������Ʈ�� �޾ƿͼ�
            if (bullet == null) yield return null;
            // ���ٸ� ����
            bullet.transform.position = firePos.position;
            bullet.GetComponent<BulletBehaviour>().Spawn();
            // ������ Ȱ��ȭ

            yield return new WaitForSeconds(coolTime);
        }
    }
}
