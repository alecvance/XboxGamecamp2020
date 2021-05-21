using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float width, startPosition;

    public GameObject cam;
    public float rate;

    public float manualDefinedWidth = 0f;   

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        if(manualDefinedWidth != 0){
            width = manualDefinedWidth;
        }else{
            width = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        }
        
        Debug.Log("width = " + width);
    }

    // Update is called once per frame
    void Update()
    {
        // how far we've travelled relative to camera //
        float temp = cam.transform.position.x * (1 - rate);

        // how far we've travelled in the world space since last update //
        float delta = cam.transform.position.x * rate;

        // move the asset //
        transform.position = new Vector3(startPosition + delta, transform.position.y, transform.position.z);

        // reposition the element if necessary //
        if (temp > startPosition + width) startPosition += width;
        else if (temp < startPosition - width) startPosition -= width;
    }
}
