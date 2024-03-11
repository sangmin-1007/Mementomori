using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    Transform Target;
    [SerializeField] float distance = 5f;
    [SerializeField] float speed = 1f;

    SpriteRenderer CharacterSprite;

    public void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        CharacterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Update()
    {
        Movement((Target.position - transform.position).magnitude);
        Rotation(Target.position.x - transform.position.x);
    }

    void Movement(float move)
    {
        // 플레이어가 일정거리 이하면 Player 추적
        if (distance > move)
        {
            Vector3 direction = Target.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    void Rotation(float rot)
    {
        // 플레이어 x 좌표에 따라 방향 전환
        if (rot > 0)
            CharacterSprite.flipX = false;
        else
            CharacterSprite.flipX = true;
    }
}