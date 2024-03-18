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

    private bool isInteract;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        ColliderSizeChanger();
    }

    private void Update()
    {
        LobbyInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<LobbyIneract>(out lobbyIneract))
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
        if (collision.TryGetComponent<GameMapInteract>(out gameMapInteract))
        {
            gameMapInteract.Interaction();
        }

        isInteract = false;
    }

    private void ColliderSizeChanger()
    {
        if(SceneManager.GetActiveScene().name == "LobbyScene")
        {
            boxCollider.size = new Vector2(1, 1);
        }
    }

    private void LobbyInteraction()
    {
        if(isInteract)
        {
            if(lobbyIneract.interactType == InteractType.Door)
            {
                lobbyIneract.Interaction();
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                lobbyIneract.Interaction();
            }
        }
    }
}
