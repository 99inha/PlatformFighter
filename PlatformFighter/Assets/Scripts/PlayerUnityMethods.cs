/* This file holds the unity methods for the player controller class
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

namespace Mechanics 
{ 
    public partial class PlayerController : MonoBehaviour
    {
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
            collided = new List<string>();
            hurtboxName = transform.GetChild(2).name;   // Hurtbox should be the 3rd on the player object's list
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            hurtboxObject = gameObject.transform.GetChild(2).gameObject;

            // linking button inputs to player number
            AxisHorizontal = "Horizontal" + playerNumber;
            AxisVertical = "Vertical" + playerNumber;
            ButtonA = "A" + playerNumber;
            ButtonB = "B" + playerNumber;
            ButtonJump = "Jump" + playerNumber;
            ButtonFall = "Fall" + playerNumber;
            ButtonShield = "Shield" + playerNumber;
        }

        // Update is called once per frame
        void Update()
        {

            correctJumpCount();
            computeShield();
            computeVulnerStates();

            // update lagTime
            lagTime -= Time.deltaTime;
            if (lagTime < 0)
                lagTime = 0f;


            if (dead)
            {
                deathTimer -= Time.deltaTime;
                if(deathTimer <= 0)
                {
                    respawn();
                    dead = false;
                }
            }

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

            computeAttackRelease();
            stablizeVertical();


            // updateAnimator();
            // only to check the exact input values
            //horizontalAxis = Input.GetAxisRaw(AxisHorizontal);
            //verticalAxis = Input.GetAxisRaw(AxisVertical);
            //velocity = rb.velocity;
        }

        private void LateUpdate()
        {   // updating hitbox
            
            if (animationStart && (animationTime == 0))
            {
                //Debug.Log("Animation:" + anime.anime.GetCurrentAnimatorStateInfo(0).length);
                animationTime = anime.anime.GetCurrentAnimatorStateInfo(0).length;

                if (attackUsed == AnimeState.DownAir)
                {
                    animationTime = 1f;
                }
            }
            else if (animationStart)
            {
                if (attackUsed != AnimeState.DownAir)
                {
                    animationTime -= Time.deltaTime;
                }
                
                if(animationTime < 0)
                {
                    if(attackHeld != AnimeState.IDLE && attackHeld != AnimeState.InAir)
                    {

                        animationStart = true;
                        animationTime = 0;
                        attackUsed = attackHeld;
                        collided.Clear();
                        generateHitBox(attackUsed);
                        
                    }
                    else
                    {
                        animationStart = false;
                        animationTime = 0;
                        collided.Clear();
                        hitboxGen = false;
                        if (isGrounded)
                        {
                            attackUsed = AnimeState.IDLE;
                        }
                        else
                        {
                            attackUsed = AnimeState.InAir;
                        }
                    }

                }
                else
                {
                    if (hitboxGen)
                    {
                        generateHitBox(attackUsed);
                    }
                }
            }

        }

        // OnCollisionEnter2D is called whenever another collider hits this object
        void OnCollisionEnter2D(Collision2D col)
        {
            if (col.transform.tag == "Ground")
            {
                anime.setAnimator(AnimeState.IDLE);
                attackUsed = AnimeState.IDLE;

                lagTime = 0f;
                animationTime = 0f;
                fallMaxSpeed = MAXFALLSPEED;
                isGrounded = true;
                jumpCount = 2;
                upBCount = 1;
            }

            else if (col.transform.tag == "Wall")
            {
                attackUsed = AnimeState.InAir;

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

        
    }
}

