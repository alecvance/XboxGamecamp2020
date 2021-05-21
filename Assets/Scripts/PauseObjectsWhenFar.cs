using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseObjectsWhenFar : MonoBehaviour
{

    public Transform trackingTarget; // can be different from above to track a predicted position
    public string tagToPause ="";
    public float pauseDistance;


    bool pauseState = false;
    GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
       objects = GameObject.FindGameObjectsWithTag(tagToPause);
      // objects = GameObject.FindSceneObjectsOfType(IPausableObject);
      Debug.Log("PauseObjectsWhenFar Found "+ objects.Length);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceLeft = trackingTarget.position - transform.position;
        distanceLeft.z = 0;

        if (distanceLeft.magnitude > pauseDistance){
            if(!pauseState){
                Debug.Log("Pausing all objects of tag " + tagToPause);
                pauseState = true;
                //pause all objects
                foreach (GameObject item in objects)
                {
                    //if(*typeof(IPausableObject).IsAssignableFrom(someOtherType))
                    // if(item is IPausableObject)

                    IPausableObject obj = item.GetComponent<IPausableObject>();
                    obj.Pause();
                }

            }
            

        }else{
            if(pauseState){
                pauseState = false;
                //unpause all objects
                Debug.Log("Un-Pausing all objects of tag " + tagToPause);

                foreach (GameObject item in objects)
                {
                    IPausableObject obj = item.GetComponent<IPausableObject>();
                    obj.Resume();
                }
            }
            
        }




    }



}

