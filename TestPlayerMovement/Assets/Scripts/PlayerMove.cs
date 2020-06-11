using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    // Declare the Rigidbody2d
    public Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get our movement values from user input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        // Create a movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Make our move using the rigidbody
        rb2d.AddForce(movement * moveSpeed);
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
