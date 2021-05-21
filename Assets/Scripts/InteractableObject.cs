using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public bool interactable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Interactible Object: " + other.name + " enter : " + this.name + " at" + Time.time);

        if(! interactable) return;
        
        if (other.tag == "Player")
        {

            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            player.interactionObject = this;

        }
    

    }


    virtual public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Interactible Object: " + other.name + " exit : " + this.name + " at" + Time.time);

        if (other.tag == "Player")
        {

            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if( player.interactionObject == this){
                player.interactionObject = null;
            }

        }
    }

    virtual public void StartInteraction(PlayerMovement player){

    }

}