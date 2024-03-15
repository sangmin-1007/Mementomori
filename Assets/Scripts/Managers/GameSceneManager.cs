using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public GameObject Player { get; private set; }
    

    public void InitializeGameScene()
    {
        var GameMap = Resources.Load<GameObject>("Prefabs/Map");
        var Player = Resources.Load<GameObject>("Prefabs/Player");
    }
    
}
