using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            Debug.Log("Player ditemukan!");
            virtualCamera.Follow = player;
            // Lakukan tindakan atau manipulasi yang diinginkan terhadap player
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan! Mencari kembali dalam 1 detik.");
            Invoke("FindPlayer", 1f); // Melakukan pencarian ulang setelah 1 detik
        }
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }
}
