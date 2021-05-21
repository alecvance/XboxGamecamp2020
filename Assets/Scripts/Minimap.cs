using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place on the Minimap Camera
public class Minimap : MonoBehaviour
{
    public Transform player;

    void LateUpdate()

    {

        //set the y position to the player position
        Vector3 newPosition = transform.position;
        newPosition.y = player.position.y;

        transform.position = newPosition;

        // compensate for player rotation (keeps minimap stable)
        Quaternion rot = player.transform.rotation;
        rot.z =  0F - rot.z;
        transform.rotation = rot;
    }
}