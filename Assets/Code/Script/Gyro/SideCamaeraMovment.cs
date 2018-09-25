using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCamaeraMovment : MonoBehaviour
{

    public Transform playerPlane;
    private Vector3 startPosition;

    void Start ()
    {
        startPosition = transform.position;
    }


    void LateUpdate()
    {
        Vector3 newPosition = playerPlane.position;
        newPosition.x = playerPlane.position.x + startPosition.x;
        newPosition.y = playerPlane.position.y + startPosition.y;
        newPosition.z = playerPlane.position.z + startPosition.z;

        transform.position = newPosition;

    }
}
