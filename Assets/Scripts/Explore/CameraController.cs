using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(0, 0, -10);
    }
}
