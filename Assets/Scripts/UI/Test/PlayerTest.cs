using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    // Update is called once per frame
    public float moveSpeed;

    private bool isShop;
    private bool isStorage;
    private bool isDoor;

    void Update()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        Vector2 inputDir = new Vector2(hAxis, vAxis).normalized;

        rb.velocity = inputDir * moveSpeed * Time.deltaTime;

        OnInteraction();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "ShopTrigger")
        {
            isShop = true;   
        }
        else if(collision.gameObject.name == "Storage")
        {
            isStorage = true;
        }
        else if(collision.gameObject.name == "DoorTrigger")
        {
            isDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ShopTrigger")
        {
            isShop = false;
        }
        else if (collision.gameObject.name == "Storage")
        {
            isStorage = false;
        }
        else if (collision.gameObject.name == "DoorTrigger")
        {
            isDoor = false;
        }
    }

    private void OnInteraction()
    {
        if(isShop)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("¼¥");
            }
        }
        else if(isStorage)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Ã¢°í");
            }
        }
        else if(isDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Managers.UI_Manager.ShowLoadingUI("GameScene");
            }
        }
    }
}
