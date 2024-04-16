using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMapInteract : MonoBehaviour
{
    public InteractType interactType;

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController PlayerInput;

    private Vector2 PlayerDir;
    private void Start()
    {
        player = Managers.GameSceneManager.Player;
        PlayerInput = Managers.GameSceneManager.Player.GetComponent<PlayerController>();

        PlayerInput.OnMoveEvent += PlayerMoveDir;
    }

    public void Interaction()
    {
        Vector3 myPos = transform.position;
        Vector3 playerPos = player.transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        float dirX = PlayerDir.x < 0 ? -1 : 1;
        float dirY = PlayerDir.y < 0 ? -1 : 1;

        switch(interactType)
        {
            case InteractType.GameMapReposition:
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 40f);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;
            default:
                break;
        }
    }

    private void PlayerMoveDir(Vector2 dir)
    {
        PlayerDir = dir;
    }

}
