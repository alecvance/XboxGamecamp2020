using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// moves in accordance to where the character (or other gameObject with Rigidbody 2D) will be in X seconds


public class PredictedPosition : MonoBehaviour
{

    public GameObject character;
    new private Rigidbody2D rigidbody2D;
    //private Vector2 predictedPosition;
    [SerializeField] private float predictedPositionTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

        //predictedPosition = transform.position;

        rigidbody2D = character.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 predictedPoint = new Vector2(character.transform.position.x, character.transform.position.y) + rigidbody2D.velocity * predictedPositionTime;
        //predictedPosition = new Vector3(predictedPoint.x, predictedPoint.y, transform.position.z);
        transform.position = new Vector3(predictedPoint.x, predictedPoint.y, character.transform.position.z);
    }
}
