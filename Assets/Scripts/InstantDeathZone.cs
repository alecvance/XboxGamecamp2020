using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathZone : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;

     void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }

     void OnTriggerEnter2D(Collider2D other){
        Debug.Log(name + ": " + other.name + " trigger enter : " + Time.time);

        if(other.tag == "Player"){

            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if(! player.dead){
                Debug.Log("You died!");
                audioSource.PlayOneShot(soundClip);

                

                player.Die();
            }
         

         }

     }
}
