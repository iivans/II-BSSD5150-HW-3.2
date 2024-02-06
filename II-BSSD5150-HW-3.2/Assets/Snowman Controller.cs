using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanController : MonoBehaviour
{
// Start is called before the first frame update
    void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Animator anim = GetComponent<Animator>();
            bool dead = anim.GetBool("dead");
            if (!dead) //if not yet dead
            {
                anim.SetBool("dead", true);
            }
            else //come back to life
            {
                anim.SetBool("dead", false);
            }
        }
    }
}