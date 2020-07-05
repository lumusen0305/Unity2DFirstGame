using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform playerTransform;
    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(playerTransform.position.x,playerTransform.position.y,-10);
    }
}
