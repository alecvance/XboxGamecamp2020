using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

    public GameObject minimap;

    // Start is called before the first frame update
    void Start()
    {
        minimap.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Minimap")){
            Debug.Log("minimap toggle");
            minimap.SetActive (! minimap.activeSelf);

        }
    }
}
