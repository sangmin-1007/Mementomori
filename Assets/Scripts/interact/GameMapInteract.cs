using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMapInteract : MonoBehaviour
{
    public InteractType interactType;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject[] tempTile;
    private GameObject[,] tiles;

    private string tileIndex;
    [SerializeField]private int curIndex_I;
    [SerializeField]private int curIndex_J;

    private void Start()
    {
        player = Managers.GameSceneManager.Player;

        tiles = new GameObject[3, 3];
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tiles[i, j] = tempTile[count];
                count++;
            }
        }

        tileIndex = this.name;
        curIndex_I = int.Parse(tileIndex[0].ToString());
        curIndex_J = int.Parse(tileIndex[1].ToString());

    }

    public void Interaction()
    {
        Vector3 myPos = transform.position;
        Vector3 playerPos = player.transform.position;

        Vector3 dir = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.up,dir);
        int sign = Vector3.Cross(transform.up, dir).z < 0 ? -1 : 1;

        angle *= sign;

        switch (interactType)
        {
            case InteractType.GameMapReposition:
                if (-45 <= angle && 45 >= angle) // À§
                {
                    for(int i = 0; i < 3; i++)
                    {
                        if(curIndex_I + 1 <= 2)
                        {
                            if (tiles[curIndex_I + 1 , i].transform.position.y - transform.position.y == -20)
                                tiles[curIndex_I + 1, i].transform.localPosition += new Vector3(0,20*3,0);
                        }
                        else
                        {
                            if (tiles[0, i].transform.position.y - transform.position.y == -20)
                                tiles[0, i].transform.position += new Vector3(0, 20 * 3, 0);
                        }
                    }
                }
                else if (45 <= angle && angle <= 135) // ¿Þ
                {
                    for(int i = 0; i < 3; i++)
                    {
                        if(curIndex_J + 1 <= 2)
                        {
                            if (tiles[i, curIndex_J + 1].transform.position.x - transform.position.x == 20)
                                tiles[i, curIndex_J + 1].transform.Translate(-Vector3.right * 60);
                        }
                        else
                        {
                            if (tiles[i, 0].transform.position.x - transform.position.x == 20)
                                tiles[i,0].transform.Translate(-Vector3.right*60);
                        }
                    }
                }
                else if (-135 >= angle || 135 <= angle) // ¾Æ·¡
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if(curIndex_I - 1 >= 0)
                        {
                            if (tiles[curIndex_I - 1, i].transform.position.y - transform.position.y == 20)
                                tiles[curIndex_I - 1, i].transform.Translate(-Vector3.up * 60);
                        }
                        else
                        {
                            if (tiles[2, i].transform.position.y - transform.position.y == 20)
                                tiles[2, i].transform.Translate(-Vector3.up * 60);
                        }
                    }
                }
                else if (-135 <= angle && angle <= -45) // ¿À
                {
                    for(int i = 0; i < 3; i++)
                    {
                        if(curIndex_J - 1 >= 0)
                        {
                            if (tiles[i, curIndex_J - 1].transform.position.x - transform.position.x == -20)
                                tiles[i, curIndex_J - 1].transform.Translate(Vector3.right * 60);
                        }
                        else
                        {
                            if (tiles[i, 2].transform.position.x - transform.position.x == -20)
                                tiles[i,2].transform.Translate(Vector3.right * 60);
                        }

                    }
                }
                break;
            default:
                break;
        }
    }
}
