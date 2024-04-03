using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneManager : MonoBehaviour
{
    public GameObject Player { get; private set; }


 
    public void IntializeLobbyScene()
    {
        var lobbyMap = Resources.Load<GameObject>("Prefabs/Map/Lobby");
        var mapInfo = Instantiate(lobbyMap).GetComponent<MapInfo>();
        
        // TODO : PlayerInstantiate
        var playerPath = Resources.Load<GameObject>("Prefabs/Player/Player");
        Player = Instantiate(playerPath, mapInfo.playerSpawnPoint.position, Quaternion.Euler(Vector3.zero));

    }
}
