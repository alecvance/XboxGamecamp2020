using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * attach this to a camera to have it follow a target (such as the player)
 */

public class FollowCameraLag : MonoBehaviour
{

    public Transform visibleTarget; // the main focus
    public Transform trackingTarget; // can be different from above to track a predicted position

    [Range(0, 1)] [SerializeField] private float paddingRatio = 0.1f; // keep the camera within a padding frame of this ratio compared to the screen
    [Range(0, 1)] [SerializeField] private float approachX = 0.9f; // how fast to approach the target (responsiveness)
    [Range(0, 1)] [SerializeField] private float approachY = 0.95f; // how fast to approach the target (responsiveness)

    Camera cam;
    Rect trackingRect; // the area of the screen that the screen position of the object that the camera is tracking should stay inside of.

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Debug.Log("Camera to World Matrix = " + cam.cameraToWorldMatrix);
        Debug.Log("Camera Rect = " + cam.rect);
        Debug.Log("Camera Pixel Rect = " + cam.pixelRect);

        Rect pixelRect = cam.pixelRect;
        float pixelWidth = cam.pixelWidth;
        float pixelHeight = cam.pixelHeight;
        float padWidth = pixelWidth * paddingRatio;
        float padHeight = pixelHeight * paddingRatio;

        trackingRect = new Rect(pixelRect.x + padWidth, pixelRect.y + padHeight, pixelRect.xMax - padWidth, pixelRect.yMax - padHeight);

    }

    // Update is called once per frame
    void Update()
    {

        // Screenspace is defined in pixels. The bottom-left of the screen is (0,0); the right-top is (pixelWidth,pixelHeight). The z position is in world units from the camera.
        Vector3 screenPoint = cam.WorldToScreenPoint(visibleTarget.position); // find the position in pixels of the target (might be offscreen!)

        if(! trackingRect.Contains(screenPoint)){
            //Debug.Log(" visible target at screen pos:   X = " + screenPoint.x + "  Y = " + screenPoint.y);

            // still need to adjust this somehow!! find out how many pixels outside the frame in each dir (x,y) the visible target is,
            // then adjust that back to world space, and use that difference to modify the trackingTarget position
            /// hmmm....
            // maybe we should put this code in the tracking target? but then it would have to know about the camera of course
        }


        Vector3 distanceLeft = trackingTarget.position - transform.position;
        distanceLeft.z = 0;

        float dX = distanceLeft.x * approachX * Time.deltaTime;
        float dY = distanceLeft.y * approachY * Time.deltaTime;

        transform.position += new Vector3(dX, dY, 0f);

     

    }
}