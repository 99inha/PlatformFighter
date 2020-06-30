/* This class functions as the controller for the player
 * Any common mechanics will be implemented here
 * ex) movement, jump, death, respawn
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UsefulConstants;

public class PlayerController : MonoBehaviour
{
    // public fields
    public PlayerHealth health;
    public AnimeState currState; // enum class AnimeState from UsefulConstants
    public float moveSpeed = 5f;
    public float jumpSpeed = 12f;
    public float fallMaxSpeed = -12f;
    public Vector2 velocity;
    public GameObject shieldObject;

    // private fields
    Shield shield;
    Rigidbody2D rb;
    Vector3 localScale;
    bool isGrounded = false;
    int jumpCount = 1;
    bool hasControl = true;
    public bool holdShield = false;


    // Unity basic functions

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        shieldObject = this.transform.Find("Shield").gameObject;
        shieldObject.SetActive(true);
        shield = shieldObject.GetComponent<Shield>();
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        correctJumpCount();

        computeShield();

        if (hasControl)
        {
            computeHorizontalMovement();
            computeVerticalMovement();
        }
    }

    // OnCollisionEnter2D is called whenever another collider hits this object
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 2;
        }
    }


    void computeShield()
    {
        bool isBroken = false;
        if (isGrounded && hasControl && Input.GetButtonDown("Shield"))  // perferrablely change this to get button instead of get key
        {
            holdShield = true;
            hasControl = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (holdShield && Input.GetButtonUp("Shield"))
        {
            holdShield = false;
            hasControl = true;
        }

        if (holdShield)
        {
            isBroken = shield.updateShield(Time.deltaTime, true);
            shieldObject.SetActive(true);

            if (isBroken)
            {           
                // Add what happens when shield is broken
                rb.velocity = new Vector2(-100, 100);
            }
        }
        else
        {
            shield.updateShield(Time.deltaTime, false);
            shieldObject.SetActive(false);

        }

    }

    // Movement computation

    void computeHorizontalMovement()
    {
        float inputDirection = Input.GetAxisRaw("Horizontal");

        if (inputDirection != 0)
        {
            localScale.x = inputDirection;
            transform.localScale = localScale;
        }
        rb.velocity = new Vector2(moveSpeed * inputDirection, rb.velocity.y);
    }

    void computeVerticalMovement()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isGrounded = false;
            jumpCount--;
        }

        if (rb.velocity.y < fallMaxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, fallMaxSpeed);
        }
    }




    /* correctJumpCount:
     *    corrects the jump count and groundedness in case a player walks off stage
     *    without jumping
     */
    void correctJumpCount()
    {
        if (isGrounded && Mathf.Abs(rb.velocity.y) > 0.05f)
        {
            isGrounded = false;
            jumpCount = 1;
        }
    }

    /* lagForSeconds
     *     strips the control over the character for a given amount of seconds
     *     Args: float lagTime: how long the character lags
     *     Returns: IEnumerator for StartCoroutine
     *     Usage: StartCoroutine("lagForSeconds", lagTime);
     */
    public IEnumerator lagForSeconds(float lagTime)
    {
        hasControl = false;
            
        yield return new WaitForSeconds(lagTime);

        hasControl = true;
    }
}
