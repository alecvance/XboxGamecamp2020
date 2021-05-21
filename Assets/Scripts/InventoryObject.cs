using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{

    public string keyName;
    public bool canHaveMultiple = false;
    public bool beingDestroyed = false;

    public AudioClip acquireSound;
    public AudioSource audioSource;

    SpriteRenderer spriteRenderer;


    void Awake()
    {
        Init();
    }

    public void Init()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(keyName +" " + "InventoryObject " + other.name + " enter : " + Time.time);

        if(beingDestroyed) return;

        if (other.tag == "Player")
        {
            // Tell Player add Key
            PlayerInventory inv = other.gameObject.GetComponent<PlayerInventory>();
            inv.addItem(keyName, canHaveMultiple);

            audioSource.PlayOneShot(acquireSound);
           // gameObject.SetActive(false); // hide it. (a fade out might be better?)
           
           //temporarily hide
           SetAlpha(0);
        

            // remove key from scene
            StartCoroutine(DestroyAfterTime(acquireSound.length));

        }

    }

// Have to let the sound finish playing first!
    IEnumerator DestroyAfterTime(float time)
    {
        beingDestroyed = true;
        yield return new WaitForSeconds(time);
        beingDestroyed = false;

        // Code to execute after the delay
        Destroy(gameObject);
    }

    public void SetAlpha(float alpha){
        Color32 c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }

    public void Conceal() {
        gameObject.SetActive(false);
    }

//Reveals from Treasure Chest and slides up if applicable
    public void Reveal() {
        gameObject.SetActive(true);

        // move up y pixels

        SlidingObject slider = gameObject.GetComponent<SlidingObject>();
        if(slider != null){
            slider.Slide();

            // disable collider while sliding
            Collider2D collider = gameObject.GetComponent<Collider2D>();
            collider.enabled = false;

            // re-enable collider after slide
            StartCoroutine(EnableColliderAfterTime(slider.movingTime));

        }

        //SetAlpha(0);


    }


    IEnumerator EnableColliderAfterTime(float time)
    {
        
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = true;

    }




}
