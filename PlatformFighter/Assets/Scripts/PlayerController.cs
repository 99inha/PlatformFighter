/* This class functions as the controller for the player
 * Any common mechanics will be implemented here
 * ex) movement, jump, death, respawn
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

namespace Mechanics 
{ 
    public class PlayerController : MonoBehaviour
    {
        public const float MAXFALLSPEED = -12f;
        public const float MAXFASTFALLSPEED = -18F;

        // public fields
        public PlayerHealth health;
        public AnimeState currState; // enum class AnimeState from UsefulConstants
        public float moveSpeed = 5f;
        public float jumpSpeed = 9f;
        public float fallMaxSpeed = -12f;
    
        public GameObject shieldObject;

        // only to check the values from Unity Engine
        public Vector2 velocity;
        public Vector3 transformRotation;
        public float horizontalAxis;
        public float verticalAxis;

        // protected fields
        protected bool hasControl = true;
        protected bool isGrounded = false;
        protected PlayerAnime anime;
        public float lagTime = 0f;
        protected Rigidbody2D rb;

        // private fields
        Shield shield;

        Vector3 localScale;
        int jumpCount = 1;
        bool holdShield = false;
        bool isDownB = false;
        bool canMove = true;
        bool canAttack = true;
        int upBCount = 1;

        bool isFacingRight = true; // this may need to be changed later on



        // Unity basic functions

        // Start is called before the first frame update
        protected void Start()
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
            correctJumpCount();
            computeShield();

            // update lagTime
            lagTime -= Time.deltaTime;
            if (lagTime < 0)
                lagTime = 0f;

            if (hasControl)
            {
                if (canMove)
                {
                    computeHorizontalMovement();
                    computeVerticalMovement();
                }
                if (lagTime == 0 && canAttack)
                {
                    computeAttacks();
                }
            }

            updateAnimator();
            // only to check the exact input values
            horizontalAxis = Input.GetAxisRaw("Horizontal");
            verticalAxis = Input.GetAxisRaw("Vertical");
            transformRotation = transform.eulerAngles;
            velocity = rb.velocity;
        }

        // OnCollisionEnter2D is called whenever another collider hits this object
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.tag == "Ground")
            {
                anime.setAnimator(AnimeState.IDLE);
                lagTime = 0f;
                fallMaxSpeed = MAXFALLSPEED;
                isGrounded = true;
                jumpCount = 2;
                upBCount = 1;
            }

            else if (col.transform.tag == "Wall")
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(0f, 0f);
                isGrounded = false;
                canAttack = false;
                jumpCount = 1;
                upBCount = 1;
            }
        }

        void OnCollisionExit2D(Collision2D col)
        {
            if (col.transform.tag == "Wall")
            {
                rb.gravityScale = 2;
                canAttack = true;
            }
        }

        // Computations

        void computeShield()
        {
            bool isBroken = false;
            if (isGrounded && hasControl && Input.GetButtonDown("Shield"))
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

        void computeHorizontalMovement()
        {
            float inputDirection = Input.GetAxisRaw("Horizontal");

            // if input direction is the opposite side while the user is grounded, flip user
            if (isGrounded && ((inputDirection == 1 && !isFacingRight) ||       // 1 is right and -1 is left
                               (inputDirection == -1 && isFacingRight)))
            {
                flip();
            }

            rb.velocity = new Vector2(moveSpeed * inputDirection, rb.velocity.y);
        }

        void computeVerticalMovement()
        {
            if (Input.GetButtonDown("Jump") && jumpCount > 0)
            {
                fallMaxSpeed = MAXFALLSPEED;
                isGrounded = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                anime.setAnimator(AnimeState.InAir);
                jumpCount--;
            }
            else if (Input.GetButtonDown("Fall") && (rb.velocity.y < 0.05) && !isGrounded)
            {
                fallMaxSpeed = MAXFASTFALLSPEED;
                rb.velocity = new Vector2(rb.velocity.x, MAXFASTFALLSPEED);
            }

            stableizeVertical();
        }

        void computeAttacks()
        {
            float vertInput = Input.GetAxisRaw("Vertical");
            float horInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("A"))
            {
                if (isGrounded)
                {
                    hasControl = false;
                    rb.velocity = new Vector2(0, 0);
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
            else if (Input.GetButtonDown("B"))
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(0, 0);

                }

                if (vertInput == 0 && horInput == 0)
                {

                    neutralB();
                }
                else if (horInput != 0)
                {
                    // if player is in air and want to side b the opposite direct that they are facing
                    if ((!isGrounded) && ((isFacingRight && horInput < 0) || (!isFacingRight && horInput > 0)))
                    {
                        flip();
                    }

                    sideB();
                }
                else if (vertInput > 0 && upBCount > 0)
                {
                    StartCoroutine("lagForSeconds", 0.1f);
                    upBCount--;
                    upB();
                }
                else if (vertInput < 0)
                {
                    isDownB = true;
                    downB();
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

        /* lagForSeconds:
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

        /* flip:
         *      flips the transform
         */
        void flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
            //UnityEngine.Debug.Log("Transform flipped");
        }



        // startAttack:
        //      Ran when the attack animation starts, it will disable movement and 
        //      add a long lagTime (so player can't buffer attack during attack animation)
        public void startAttack()
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, 0);
                hasControl = false;
            }
            lagTime = 3f;  // big attack so players can't buffer an attack during the attack animation

        }


        // attackLag:
        //      Ran at the end of a attack animation to give the attack some end lag
        //      if the input time is 0, the function will not give control back, it is 
        //      used for attacks that doesn't end when the attack animation time is up
        //      example: attack that can be held
        public void attackLag(float time)
        {
            if (time != 0)
            {
                lagTime = time;

                hasControl = true;
            }

        }

        public void giveControl()
        {
            hasControl = true;
        }
    
        public void setVerticalVelocity(float value)
        {
            rb.velocity = new Vector2(0, value);
            fallMaxSpeed = Mathf.Min(value, fallMaxSpeed);
        }

        public void updateAnimator()    // used to tell the animator that the B-button is released
        {
            if (Input.GetButtonUp("B") && isDownB)
            {
                isDownB = false;
                releaseDownB();
            }
        }

        public void stableizeVertical()
        {
            if (rb.velocity.y < fallMaxSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, fallMaxSpeed);
            }
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

        protected virtual void neutralB()
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

        protected virtual void releaseDownB()
        {

        }
    }
}

