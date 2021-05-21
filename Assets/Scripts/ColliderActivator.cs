using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{

    public Collider2D otherCollider;

    
     void OnTriggerEnter2D(Collider2D other){
        Debug.Log(name + ": " + other.name + " trigger enter : " + Time.time);

       

        if(other.tag == "Player"){

                otherCollider.enabled = true;

         }

     }
}
