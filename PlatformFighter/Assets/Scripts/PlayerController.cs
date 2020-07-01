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
    public float jumpSpeed = 9f;
    public float fallMaxSpeed = -12f;
    public Vector2 velocity;

    // private fields
    Rigidbody2D rb;
    Vector3 localScale;
    bool isGrounded = false;
    int jumpCount = 1;
    bool hasControl = true;


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




    // Movement computation

    void computeHorizontalMovement()
    {
        float inputDirection = Input.GetAxisRaw("Horizontal");

        if (inputDirection != 0 && isGrounded)
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
