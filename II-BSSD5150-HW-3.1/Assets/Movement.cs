using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private int health = 3;

    [SerializeField]
    private Transform spawnPoint;

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

        if (health <= 0)
        {
            Debug.Log("You Lose!");
            // COULD DESTROY PLAYER
        }

        if (movement.x < 0)
        {
            // Moving left, flip the sprite
            transform.localScale = new Vector3(-5, 5, 1);
        }
        else if (movement.x > 0)
        {
            // Moving right, restore the original sprite scale
            transform.localScale = new Vector3(5, 5, 1);
        }
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

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionExit2D(Collision2D collison)
    {
        grounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        //Debug.Log("You hit:" + collison.gameObject.tag);

        if (collison.gameObject.tag == "Enemy")
        {
            gameObject.transform.position = spawnPoint.position;
            health--;
            if (health < 0)
            {
                health = 0;
            }
        }

        else if (collison.gameObject.tag == "HitPoint")
        {
            if (collison.gameObject != null)
            {
                Destroy(collison.gameObject.transform.parent.gameObject);
            }
        }

        else if (collison.gameObject.tag == "Goal" && health > 0)
        {
            Debug.Log("You Win!");
        }
    }
}



