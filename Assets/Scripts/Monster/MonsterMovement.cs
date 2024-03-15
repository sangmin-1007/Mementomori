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
        // Player ����
        Vector3 direction = Target.position - transform.position;
        transform.position += direction.normalized * speed * Time.deltaTime;

        // ���� ��ȯ
        if (Target.position.x - transform.position.x > 0)
            CharacterSprite.flipX = false;
        else
            CharacterSprite.flipX = true;
    }
}