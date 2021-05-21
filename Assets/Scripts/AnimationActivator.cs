using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
  
    public Animator animator;

     void OnTriggerEnter2D(Collider2D other){
        Debug.Log(name + ": " + other.name + " trigger enter : " + Time.time);


        if(other.tag == "Player"){

                animator.enabled = true;

         }

     }
}
