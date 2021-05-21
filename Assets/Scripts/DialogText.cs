using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]

public class DialogText : MonoBehaviour
{
    public TMP_Text textObject;
    Image BGimage;

    // Start is called before the first frame update
    void Start()
    {
        BGimage = GetComponent<Image>();
        //textObject = GetComponent<TextMeshPro>();
        textObject.text = ""; // clear
    }

    // Update is called once per frame
    void Update()
    {
        // This is kind of an inefficient hack to check every frame for -- but I am in a hurry

        if(textObject.text == ""){
            BGimage.enabled = false;
        }else{
            BGimage.enabled = true;
        }
    }

    public void SetText(string text){
        textObject.text = text;
    }
}
