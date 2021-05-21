using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
// Open door when enter zone; 
//   optionally close when leave.
//   optionally use key to open.
*/

public class DoorTrigger : MonoBehaviour
{
    public Door door;

    //Whether or not the door will close again when you leave the trigger zone
    public bool closesOnExit = true;


    //Whether or not the door will open again when you leave the trigger zone
    public bool opensOnEnter = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.E)){
            door.OpenDoor();
        } */
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log(other.name + " enter : " + Time.time);

        if(opensOnEnter){
            door.TryOpenBy(other);
        }
     }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name + " exit : " + Time.time);
       
        if (door.state != Door.OCObjectState.open) return;

        if(closesOnExit){ 

            if (other.tag == "Player")
            {
                door.Close();
            }
        }

    }


}
