using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThrowWeapon : MonoBehaviour
{
    [SerializeField] private GameObject sowerd;

    public Sprite[] sowerdSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float power;
    [SerializeField] private float rotate;

    Transform bullet;

    private GameObject player;
    [SerializeField] private float time = 0f;
    private void Awake()
    {
        player = Managers.GameSceneManager.Player;
    }
    private void OnEnable()
    {
        transform.position = player.transform.position;
        int randIndex = Random.Range(0, sowerdSprite.Length);
        spriteRenderer.sprite = sowerdSprite[randIndex];

        Vector2 weaponDir = Vector2.up;
        int rand = Random.Range(0, 3);

        weaponDir.x -= 0.2f;
        weaponDir.x += 0.2f * rand;

        rb.AddForce(weaponDir * power);
    }

    private void Update()
    {
        if (!sowerd.activeSelf)
            return;

        transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
        DisableSkill();
    }

    private void DisableSkill()
    {
        time += Time.deltaTime;

        if (time >= 3)
        {
            sowerd.SetActive(false);
            time = 0;
        }
    }

}
