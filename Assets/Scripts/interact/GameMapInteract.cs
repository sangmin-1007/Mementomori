using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMapInteract : MonoBehaviour
{
    public InteractType interactType;

    [SerializeField] private GameObject player;
    [SerializeField] private PlayerController PlayerInput;


    private Vector2 PlayerDir;
    private Vector3 upRight;
    private Vector3 myPos;
    private Vector3 playerPos;
    private void Start()
    {
        player = Managers.GameSceneManager.Player;
        PlayerInput = Managers.GameSceneManager.Player.GetComponent<PlayerController>();

        PlayerInput.OnMoveEvent += PlayerMoveDir;

        upRight = new Vector3(1, 1, 0);
    }

    public void Update()
    {
        myPos = transform.position;
        playerPos = player.transform.position;
    }

    public void Interaction()
    {

        switch (interactType)
        {
            case InteractType.GameMapReposition:

                float dirX = playerPos.x  - myPos.x;
                float dirY = playerPos.y - myPos.y;

                float diffX = Mathf.Abs(dirX);
                float diffY = Mathf.Abs(dirY);

                dirX = dirX > 0 ? 1 : -1;
                dirY = dirY > 0 ? 1 : -1;


                if (Mathf.Abs(diffX - diffY) <= 0.1f || diffX == diffY)
                {
                    transform.Translate(Vector3.right * dirX * 80f);
                    transform.Translate(Vector3.up * dirY * 80f);
                }
                else if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX  * 80f);
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY  * 80f);
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
