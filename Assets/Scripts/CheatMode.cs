using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatMode : MonoBehaviour
{

    public List<GameObject> locations;
    public int nextLocation = 0;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = locations[nextLocation].transform.position;
            nextLocation ++;
            if(nextLocation >= locations.Count) nextLocation = 0;

        } 

    }
}
