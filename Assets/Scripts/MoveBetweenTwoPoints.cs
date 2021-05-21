using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenTwoPoints : SlidingObject
{

    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        base.Slide();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        if(state == SlidingState.there){

            // swap origin and destination
            Vector3 temp = origin;
            origin = fixedDestination;
            fixedDestination = temp;

            moveVector = fixedDestination - origin;

            state = SlidingState.here;

            base.Slide();

        }
    }
}
