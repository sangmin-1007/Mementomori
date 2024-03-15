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
            // 오브젝트를 받아와서
            if (bullet == null) yield return null;
            // 없다면 종료
            bullet.transform.position = firePos.position;
            bullet.GetComponent<BulletBehaviour>().Spawn();
            // 있으면 활성화

            yield return new WaitForSeconds(coolTime);
        }
    }
}
