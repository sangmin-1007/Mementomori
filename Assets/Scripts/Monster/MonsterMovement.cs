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
        // �÷��̾ �����Ÿ� ���ϸ� Player ����
        if (distance > move)
        {
            Vector3 direction = Target.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    void Rotation(float rot)
    {
        // �÷��̾� x ��ǥ�� ���� ���� ��ȯ
        if (rot > 0)
            CharacterSprite.flipX = false;
        else
            CharacterSprite.flipX = true;
    }
}