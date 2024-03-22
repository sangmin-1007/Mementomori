//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class RangedAttack : MonoBehaviour
//{
//    Animator animator;
//    SpawnManager spawnManager;

//    protected PlayerStatsHandler Stats { get; private set; }
//    [SerializeField] private string targetTag = "Player";

//    private HealthSystem healthSystem;
//    private HealthSystem playerHealthSystem;

//    private void Awake()
//    {
//        animator = GetComponentInChildren<Animator>();

//        healthSystem = GetComponent<HealthSystem>();
//        Stats = GetComponent<PlayerStatsHandler>();
//    }

    

//    float DistanceToTarget()
//    {
//        return Vector3.Distance(transform.position, GameManager.position);
//    }

//    void OnShoot()
//    {

//    }
//}
