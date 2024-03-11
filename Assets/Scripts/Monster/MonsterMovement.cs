using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    Transform Target;
    [SerializeField] float speed = 1f;

    SpriteRenderer CharacterSprite;

    public void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        CharacterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void Update()
    {
        // Player 추적
        Vector3 direction = Target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        // 방향 전환
        if (Target.position.x - transform.position.x > 0)
            CharacterSprite.flipX = false;
        else
            CharacterSprite.flipX = true;
    }
}