using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField]
    private bool x, y, z;

    private Transform target;

    private void Start()
    {
        target = Managers.GameSceneManager.Player.GetComponent<Transform>();
    }

    private void Update()
    {
        if (!target)
            return;

        transform.position = new Vector3(
            (x ? target.position.x : transform.position.x),
            (y ? target.position.y : transform.position.y),
            (z ? target.position.z : transform.position.z));
    }
}
