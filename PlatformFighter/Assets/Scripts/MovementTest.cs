using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;

public class MovementTest : MonoBehaviour
{
    const int LOCALSCALE_RIGHT = 1;
    const int LOCALSCALE_LEFT = -1;
    public float moveSpeed = 5f;
    Vector3 localScale; // for changing direction
    Animator anime;

    public PlayerHealth health;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (Input.GetKey(KeyCode.A))
        {
            localScale.x = LOCALSCALE_LEFT;
            transform.localScale = localScale;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            localScale.x = LOCALSCALE_RIGHT;
            transform.localScale = localScale;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
        if (Input.GetKey(KeyCode.J))
        {
            anime.SetTrigger("attack");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            anime.ResetTrigger("attack");

        }
        */
    }


    public void takeDamage(Attack attack)
    {
        Vector2 finalKnockback = health.takeDamage(attack);
        rb.velocity = finalKnockback;

        Debug.Log("damage: "+ attack.damage + ", knockBack: " + finalKnockback.x + "," + finalKnockback.y);
    }
}
