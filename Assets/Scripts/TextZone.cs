using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextZone : MonoBehaviour
{

    public TMP_Text textObject; // should probably change this to use the DialogText class  
    public string message;

    public float timeToShow = 3.0f;
    public bool showAgain = true;

    private bool alreadyShown = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + ": " + other.name + " trigger enter : " + Time.time);

        if (other.tag == "Player")
        {

            if(! alreadyShown || showAgain){

                alreadyShown = true;

                PlayerMovement player = other.GetComponent<PlayerMovement>();

                textObject.text = message;
                
                StartCoroutine(HideMessageAfterTime(timeToShow));
        }


        }

    }


    
    IEnumerator HideMessageAfterTime(float time)
    {
    
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        if (textObject.text == message)
        {
            textObject.text = "";
        }
    }

}
