using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private Transform Groundcheck;

    [SerializeField]
    private LayerMask GroundLayer;

    [SerializeField]
    private float speed = 5.0f; //speed of car

    [SerializeField]
    private float jumpForce = 2.0f; //speed of car

    private float checkRadius = 0.01f;

    private Rigidbody2D rb2d; //holder for rigidboy, accessible between multiple functions

    private bool grounded = true;

    void Start()// Start is called before the first frame update
    {
        rb2d = GetComponent<Rigidbody2D>(); //retrieve the RB2D component once only
    }

    private void Update()
    {
        Vector2 movement = rb2d.velocity;

        if (Input.GetKeyDown("space") && grounded)
        {
            movement.y = jumpForce;
        }
        rb2d.velocity = movement;
    }

// Update is called once per frame
void FixedUpdate()
    {

        grounded = Physics2D.OverlapCircle(Groundcheck.position, checkRadius, GroundLayer);

        float h = Input.GetAxisRaw("Horizontal"); //only testing for right and left arrow
        float v = Input.GetAxisRaw("Vertical"); //only testing for up and down arrow
        Vector2 movement = rb2d.velocity;

        if (grounded)
        {
            movement.x = h * speed;
        }
        else
        {
            movement.x = h * speed / 2;
        }

        /*
        if (v != 0 && grounded)
        {
            movement.y = v * jumpForce;
        }
        rb2d.velocity = movement;
        */
        rb2d.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collison)
    {
        grounded = false;
    }
}