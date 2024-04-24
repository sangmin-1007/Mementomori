using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BullletType
{
    Long,
    Shot,
    Throw
}

public class Skill_Bullet : MonoBehaviour
{
    public float damage;
    public int per;
    public BullletType bullletType;

    public float timer;

    Rigidbody2D rigid;
    PlayerStatsHandler playerStats;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            playerStats = Managers.GameSceneManager.Player.GetComponent<PlayerStatsHandler>();
        }
    }
    private void Update()
    {
        if(bullletType == BullletType.Long)
        {
            DisableSkill();
        }
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage + playerStats.allAttack;
        this.per = per;

        if(per > -1)
        {
            rigid.velocity = dir * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bullletType == BullletType.Long)
        {
            if (!collision.CompareTag("Enemy") || per == -1)
                return;
            else
            {
                HealthSystem healthSystem1 = collision.GetComponent<HealthSystem>();
                if (healthSystem1 != null)
                {
                    healthSystem1.ChangeHealth(-damage);

                }
            }
            per = per - 1;
            if (per == -1)
            {
                rigid.velocity = Vector2.zero;
                gameObject.SetActive(false);
            }
        }
        
        else if (bullletType == BullletType.Shot)
        {
            if (!collision.CompareTag("Enemy"))
                return;
            else
            {
                HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.ChangeHealth(-damage);
                }
            }
        }
        else if(bullletType == BullletType.Throw)
        {
            if (!collision.CompareTag("Enemy"))
                return;
            else
            {
                HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
                if (healthSystem != null)
                {
                    healthSystem.ChangeHealth(-damage);
                }
            }
        }
    }

    private void DisableSkill()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            gameObject.SetActive(false);
            timer = 0;
        }
    }
}
