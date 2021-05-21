using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for Doors and Treasure Chests
public class OpenAndCloseableObject : InteractableObject
{
    private BoxCollider2D boxCollider;

    [HideInInspector] public Animator animator;

    public enum OCObjectState { closed, opening, open, closing };

    public OCObjectState state = OCObjectState.closed;

    public string needsKeyNamed;
    public bool consumesKey;
    // this is true when the Coroutine is executing
    public bool beingUnlocked = false;

    public AudioClip unlockSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init(){
        animator = gameObject.GetComponent<Animator>();
        if(audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        if (state == OCObjectState.open)
        {
            animator.SetBool("Opening", true);
            animator.SetTrigger("StartOpened");
        }


    }
    // Update is called once per frame
    void Update()
    {

    }

    virtual public void Open()
    {
        Debug.Log(gameObject.name + " open : " + Time.time);

        animator.SetBool("Opening", true);

        if (state != OCObjectState.open) state = OCObjectState.opening;

    }


    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    virtual public void Close()
    {
        Debug.Log(gameObject.name + " close : " + Time.time);

        animator.SetBool("Opening", false);

        if (state != OCObjectState.closed) state = OCObjectState.closing;

    }

   public void TryOpenBy(Collider2D other){


        if (beingUnlocked) return;

        if (state != OCObjectState.closed) return;

        if (other.tag == "Player")
        {
            if (needsKeyNamed != "")
            {

                // if we need a key, see if the player has it in their inventory
                // and remove it (if required) if they do before opening door.


                PlayerInventory inv = other.gameObject.GetComponent<PlayerInventory>();
                if (inv.hasItem(needsKeyNamed))
                {
                    // key needed

                    //play unlock sound
                    audioSource.PlayOneShot(unlockSound);

                    if (consumesKey)
                    {
                        inv.removeItem(needsKeyNamed);
                    }

                    // delay this to let key sound complete
                    StartCoroutine(OpenAfterTime(unlockSound.length));

                }
                else
                {
                    // play a warning sound or give a message that you don't have the key you need
                }

            }
            else
            {
                // no key needed

                Open();

            }

            

        }

    }


    IEnumerator OpenAfterTime(float time)
    {
        beingUnlocked = true;
        yield return new WaitForSeconds(time);
        beingUnlocked = false;

        // Code to execute after the delay
        Open();
    }



    /*
        override public void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Door: " + other.name + " enter : " + this.name + " at" + Time.time);

           // if (state == DoorState.open)
            {
                base.OnTriggerEnter2D(other);

            }

        }


        override public void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Door: " + other.name + " exit : " + this.name + " at" + Time.time);

            //if (state == DoorState.open)
            {
                base.OnTriggerExit2D(other);
            }

        }
    
    */


}
