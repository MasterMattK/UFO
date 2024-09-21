// Matthew Kellen, 9/21/2024
// This script controls the position of the camera

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    private void Start()
    {
        offset = transform.position - player.transform.position; // offset vector from initial config
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
