using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTest : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public float moveSpeed;

    private bool isShop;
    private bool isStorage;
    private bool isDoor;

    private LobbyIneract interact;

    private bool isInteract;

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(hAxis, vAxis).normalized;

        rb.velocity = inputDir * moveSpeed * Time.deltaTime;

        Interaction();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<LobbyIneract>(out interact))
        {
            isInteract = true;
        }
        else
        {
            isInteract = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInteract = false;
    }

    private void Interaction()
    {
        if(isInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                interact.Interaction();
            }
        }

    }

}
