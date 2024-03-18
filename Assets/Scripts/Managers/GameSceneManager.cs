using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject Player { get; private set; }
    

    public void InitializeGameScene()
    {
        var gameMap = Resources.Load<GameObject>("Prefabs/Map/GameMap");
        var mapInfo = Instantiate(gameMap).GetComponent<MapInfo>();

        var playerPath = Resources.Load<GameObject>("Prefabs/Player/PlayerTest");
        Player = Instantiate(playerPath, mapInfo.playerSpawnPoint.position, Quaternion.Euler(Vector3.zero));

    }
    
}