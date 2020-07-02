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
    public GameObject shieldObject;


    public float horizontalAxis;
    public float verticalAxis;

    // protected fields
    protected bool hasControl = true;
    protected bool isGrounded = false;
    protected PlayerAnime anime;
    public float lagTime = 0f;

    // private fields
    Shield shield;
    Rigidbody2D rb;
    Vector3 localScale;
    int jumpCount = 1;
    bool holdShield = false;

    bool isFacingRight = true; // this may need to be changed later on



    // Unity basic functions

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;
        //shieldObject = this.transform.Find("Shield").gameObject;
        shieldObject.SetActive(true);
        shield = shieldObject.GetComponent<Shield>();
        anime = GetComponent<PlayerAnime>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;
        correctJumpCount();

        computeShield();
        

        // update lagTime
        lagTime -= Time.deltaTime;
        if (lagTime < 0)
            lagTime = 0f;

        if (hasControl)
        {
            computeHorizontalMovement();
            computeVerticalMovement();
            if(lagTime == 0)
            {
                computeAttacks();
            }
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
            anime.setAnimator(AnimeState.IDLE);
            lagTime = 0f;
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
            anime.setAnimator(AnimeState.InAir);

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
            anime.setAnimator(AnimeState.InAir);
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
        //hasControl = false;
            
        yield return new WaitForSeconds(lagTime);

        hasControl = true;
    }


    public void startAttack()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(0, 0);
            hasControl = false;
        }

        lagTime = 10f;  // big attack so players can't buffer an attack during the attack animation

    }

    public void attackLag(float time)
    {
        lagTime = time;

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
