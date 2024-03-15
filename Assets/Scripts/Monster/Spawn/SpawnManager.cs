using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public PoolManager pool;
    public PlayerInputController player;

    private void Awake()
    {
        instance = this;
    }
}
