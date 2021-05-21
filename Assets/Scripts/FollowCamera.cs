using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public Transform cameraTransform; // could actually use this on any transform
    public float ratio = 1.0f;
    public float y_ratio = 1.0f;

    public Vector3 v_Ratio;
    Vector3 startPos;
    Vector3 cameraStartPos;

    Vector3 offset;

    

    // Start is called before the first frame update
    void Start()
    {
        v_Ratio = new Vector3(ratio, y_ratio, 0f);

        startPos = transform.position;
        cameraStartPos = cameraTransform.position;
        offset = cameraStartPos -  startPos;

    }

    // Update is called once per frame
    void Update()
    {
//        transform.position = cameraTransform.position + offset;
  
        Vector3 totalVector = cameraTransform.position - cameraStartPos;

        transform.position = startPos + Vector3.Scale(totalVector, v_Ratio);

    }
}
