using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ActiveTime(3f));
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator ActiveTime(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        gameObject.SetActive(false);
    }
}