using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : OpenAndCloseableObject {
    public Door otherDoor; // if connected to another door somewhere.

    // Start is called before the first frame update
    void Start()
    {
        base.Init();

        if(otherDoor == null) interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Open(){
        Debug.Log( gameObject.name + " open door: " + Time.time);

        base.Open();

        if(otherDoor != null){
            otherDoor.OpenPairedDoor();
        }
    }
    public void OpenPairedDoor()
    {
        Debug.Log(gameObject.name + " open paired door: " + Time.time);

        base.Open();

    }


    override public void Close(){
        Debug.Log( gameObject.name + " close door: " + Time.time);

       base.Close();
       
        if (otherDoor != null)
        {
            otherDoor.ClosePairedDoor();
        }
    }

    public void ClosePairedDoor(){
        Debug.Log(gameObject.name + " close paired door: " + Time.time);

        base.Close();

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
    override public void StartInteraction(PlayerMovement player)
    {
        if (state == OCObjectState.open){

            //move player to new door position.
            Vector3 newPos = otherDoor.transform.position;

            /*
            // put player "behind" new doors, if new door is not open
            if(otherDoor.state != DoorState.open){
                newPos.z = otherDoor.transform.position.z + 1;
            }
            */
            

            player.transform.position = newPos;

            //


        }
    }

}
