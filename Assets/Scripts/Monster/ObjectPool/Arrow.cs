using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Invoke("DestroyArrow", 5f);
    }

    public void DestroyArrow()
    {
        ObjectPool.ReturnObject(this);
    }

    void Update()
    {
        transform.Translate(direction);
    }
}
