using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Camera _camera;

    private Transform targetTransform;

    private void Start()
    {
        _camera = Camera.main;
        targetTransform = gameObject.transform;
    }

    private void Update()
    {
        Vector3 playerPos = new Vector3(targetTransform.position.x, targetTransform.position.y, _camera.transform.position.z);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position,playerPos, speed * Time.deltaTime);
    }
}

