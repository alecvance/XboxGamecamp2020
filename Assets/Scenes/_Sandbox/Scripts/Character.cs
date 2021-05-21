using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is to be agnostic with all properties and methods being global to a game character
/// </summary>
public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool isGrounded = true;
    
    public static void enableHorizMovement(Rigidbody2D rBody, float speed)
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rBody.AddForce(new Vector2(speed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rBody.AddForce(new Vector2(-speed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }
    }

    public static void enableJumpMovement(Rigidbody2D rBody, float height)
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rBody.AddForce(new Vector2(0, height * Time.deltaTime), ForceMode2D.Impulse);
            //print(isGrounded);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var colidName = collision.collider.name;
        if(colidName == "ground")
        {
            isGrounded = true;
            //print("Change state to true");
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        //print("Change state to false");
    }


}
