using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private Transform spawnPoint;

    Rigidbody2D m_Rigidbody;
    public float m_Speed = 5f;
    Animator anim;
    Collider2D swordCollider;

    void Start()
    {
        // Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey("space"))
        {
            anim.SetBool("Attacking", true);
        }
        else
        {
            anim.SetBool("Attacking", false);
        }
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            swordCollider.enabled = true;
        }
        else
        {
            swordCollider.enabled = false;
        }

        // Store user input as a movement vector
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 m_Input = new Vector3(h, v, 0);

        if (h != 0 || v != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        // Apply the movement vector to the current position, which is
        // multiplied by deltaTime and speed for a smooth MovePosition
        m_Rigidbody.MovePosition(transform.position + m_Input * Time.deltaTime * m_Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (anim.GetBool("Attacking"))
        {
            //Debug.Log("Setting 'dead' boolean to true");
            collision.gameObject.GetComponent<Animator>().SetBool("dead", true);
        }

        if (collision.CompareTag("CliffTrigger"))
        {
            m_Rigidbody.gravityScale = 10f; // Turn on gravity when exiting the cliff trigger
        }

        if (collision.CompareTag("OutOfBound"))
        {
            // Reset the player's position to the spawn point
            transform.position = spawnPoint.position;
            m_Rigidbody.gravityScale = 0f;
        }
    }
}
