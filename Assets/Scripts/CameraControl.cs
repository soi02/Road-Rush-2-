using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Transform playerTr;
    float dist = 1.0f;
    float height = 2.0f;
    float dampTrace = 20.0f;
    Transform cameraTr;

    void Start()
    {
        cameraTr = Camera.main.GetComponent<Transform>();
        playerTr = GetComponent<Transform>();
    }

    void Update()
    {
        cameraTr.position = Vector3.Lerp(cameraTr.position,
            playerTr.position - (playerTr.forward * dist) + (Vector3.up * height) + (Vector3.forward * 8),
            Time.deltaTime * dampTrace);
        cameraTr.LookAt(playerTr.position + Vector3.up + Vector3.right * 3);
    }
}
