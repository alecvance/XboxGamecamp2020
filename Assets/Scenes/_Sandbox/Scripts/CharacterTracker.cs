using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{


    // Scene's Camera
    public GameObject character;



    // Start is called before the first frame update
    void Start()
    {
        print("Starting character position " + character.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {

        // gameCamera tracks character
        print("Starting character position " + character.transform.position.x);
        print("Camera position " + transform.position.x);
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -50);

    }
}
