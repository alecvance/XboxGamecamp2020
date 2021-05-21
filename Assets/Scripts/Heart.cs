using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : InventoryObject
{

    public int hitPointsToAdd;

    // Start is called before the first frame update
    void Awake()
    {
        base.Init();
    }

     public override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(keyName +" " + "Heart " + other.name + " enter : " + Time.time);

        if(beingDestroyed) return;

        if (other.tag == "Player")
        {
         
            base.OnTriggerEnter2D(other);

            // add HP to player
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.RestoreHP(hitPointsToAdd);

        }

    }
   
}
