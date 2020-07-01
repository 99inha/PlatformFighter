﻿/* This class functions as the controller for the player
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
    public float jumpSpeed = 9f;
    public float fallMaxSpeed = -12f;
    public Vector2 velocity;

    public float horizontalAxis;
    public float verticalAxis;

    // protected fields
    protected bool hasControl = true;
    protected bool isGrounded = false;

    // private fields
    Rigidbody2D rb;
    Vector3 localScale;
    int jumpCount = 1;
    bool isFacingRight = true; // this may need to be changed later on
    


    // Unity basic functions

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        correctJumpCount();

        if (hasControl)
        {
            computeHorizontalMovement();
            computeVerticalMovement();
            computeAttacks();
        }

        // only to check the exact input values
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
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




    // Computations

    void computeHorizontalMovement()
    {
        float inputDirection = Input.GetAxisRaw("Horizontal");

        if (inputDirection != 0 && isGrounded)
        {
            localScale.x = inputDirection;
            transform.localScale = localScale;

            isFacingRight = (localScale.x == 1);
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

    void computeAttacks()
    {
        float vertInput = Input.GetAxisRaw("Vertical");
        float horInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("A"))
        {
            if (isGrounded)
            {
                if (vertInput == 0 && horInput == 0)
                {
                    jab();
                }
                else if (horInput != 0)
                {
                    fTilt();
                }
                else if (vertInput > 0)
                {
                    upTilt();
                }
                else if (vertInput < 0)
                {
                    downTilt();
                }
            }

            else
            {
                if (vertInput == 0 && horInput == 0)
                {
                    nair();
                }
                else if ((isFacingRight && horInput > 0) || (!isFacingRight && horInput < 0))
                {
                    fair();
                }
                else if ((isFacingRight && horInput < 0) || (!isFacingRight && horInput > 0))
                {
                    bair();
                }
                else if (vertInput > 0)
                {
                    upair();
                }
                else if (vertInput < 0)
                {
                    downair();
                }
            }
        }
    }

    // helper functions

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



    // attacks

    protected virtual void jab()
    {

    }

    protected virtual void fTilt()
    {

    }

    protected virtual void upTilt()
    {

    }

    protected virtual void downTilt()
    {

    }

    protected virtual void nair()
    {

    }

    protected virtual void fair()
    {

    }

    protected virtual void bair()
    {

    }

    protected virtual void upair()
    {

    }

    protected virtual void downair()
    {

    }

    protected virtual void sideB()
    {

    }

    protected virtual void upB()
    {

    }

    protected virtual void downB()
    {

    }
}
