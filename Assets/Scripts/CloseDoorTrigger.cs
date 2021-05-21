using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
// Close door when enter zone; 
*/

public class CloseDoorTrigger : MonoBehaviour
{

    public Door door;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (door.state != OpenAndCloseableObject.OCObjectState.open) return;

        Debug.Log(gameObject.name +" : " + other.name + " enter : " + Time.time);

        if (other.tag == "Player")
        {
        
                door.Close();
    
        }

    }
}
