using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Main Character Class inherits from the Character Class
/// </summary>
public class MainCharacter : Character
{
    public Rigidbody2D _rigidBody;
    public float speed;
    public float jumpHeight = 150;




    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
      
    }
    void Update()
    {

    }

    void FixedUpdate()
    {

        Character.enableHorizMovement(_rigidBody, speed);
        Character.enableJumpMovement(_rigidBody,jumpHeight);

    }


}
