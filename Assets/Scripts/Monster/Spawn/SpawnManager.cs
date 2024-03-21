using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PoolManager pool;
    private PlayerInputController player;

    private void Start()
    {
        player = Managers.GameSceneManager.Player.GetComponent<PlayerInputController>();
    }
}
