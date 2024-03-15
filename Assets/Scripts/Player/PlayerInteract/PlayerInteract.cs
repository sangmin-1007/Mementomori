using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
    private GameMapInteract gameMapInteract;
    private LobbyIneract lobbyIneract;

    private BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        ColliderSizeChanger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<GameMapInteract>(out gameMapInteract))
        {
            gameMapInteract.Interaction();
        }
    }

    private void ColliderSizeChanger()
    {
        if(SceneManager.GetActiveScene().name == "LobbyScene-KSM")
        {
            boxCollider.size = new Vector2(1, 1);
        }
    }
}
