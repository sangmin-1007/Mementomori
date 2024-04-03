using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    public int id;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per > -1)
        {
            rigid.velocity = dir * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
        {
            HealthSystem healthSystem1 = collision.GetComponent<HealthSystem>();
            if (healthSystem1 != null)
            {
                healthSystem1.ChangeHealth(-damage);
            }
        }

        per--;

        if (per == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

        HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
        if(healthSystem != null )
        {
            healthSystem.ChangeHealth(-damage);
        }
    }
}
