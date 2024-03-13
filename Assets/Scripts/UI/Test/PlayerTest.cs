using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    // Update is called once per frame
    public float moveSpeed;
    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(hAxis, vAxis).normalized;

        rb.velocity = inputDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ShopTrigger")
        {
            Debug.Log("shop");
        }
        else
        {
            Debug.Log("æ¿ ¿Ãµø!");
        }
    }
}
