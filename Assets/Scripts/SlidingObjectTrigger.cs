using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObjectTrigger : MonoBehaviour
{

    public SlidingObject slidingObject;

    //Whether or not the object will return when you leave the trigger zone
    public bool returnsOnExit = false;

    public string needsKeyNamed;
    public bool consumesKey;

    public AudioClip unlockSound;

    private AudioSource audioSource;

// this is true when the Coroutine is executing
    public bool beingUnlocked = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(name + ":" + other.name + " enter : " + Time.time);

        if (beingUnlocked) return;

        if(slidingObject.state != SlidingObject.SlidingState.here) return;

        if(other.tag == "Player"){

           

            if(needsKeyNamed!=""){

                // if we need a key, see if the player has it in their inventory
                // and remove it (if required) if they do before opening door.


                PlayerInventory inv = other.gameObject.GetComponent<PlayerInventory>();
                if(inv.hasItem(needsKeyNamed)){
                    // key needed

                    //play unlock sound
                    audioSource.PlayOneShot(unlockSound);

                    if(consumesKey){
                        inv.removeItem(needsKeyNamed);
                    }

                    // delay this to let key sound complete
                    StartCoroutine(StartMovingAfterTime(unlockSound.length));

                }else{
                    // play a warning sound or give a message that you don't have the key you need
                }

            }else{
                // no key needed

                slidingObject.Slide();

            }


         }

     }

    IEnumerator StartMovingAfterTime(float time)
    {
        beingUnlocked = true;
        yield return new WaitForSeconds(time);
        beingUnlocked = false;
        
        // Code to execute after the delay
        slidingObject.Slide();
    }
}
