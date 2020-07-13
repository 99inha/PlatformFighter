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

            computeAttackRelease();
            stablizeVertical();


            // updateAnimator();
            // only to check the exact input values
            horizontalAxis = Input.GetAxisRaw("Horizontal");
            verticalAxis = Input.GetAxisRaw("Vertical");
            transformRotation = transform.eulerAngles;
            velocity = rb.velocity;
        }

        private void LateUpdate()
        {   // updating hitbox
            
            if (animationStart && (animationTime == 0))
            {
                Debug.Log("Animation Timeloolololol:" + anime.anime.GetCurrentAnimatorStateInfo(0).length);
                animationTime = anime.anime.GetCurrentAnimatorStateInfo(0).length;
            }
            else if (animationStart)
            {
                animationTime -= Time.deltaTime;
                if(animationTime < 0)
                {
                    animationStart = false;
                    animationTime = 0;
                    collided.Clear();
                    if (isGrounded)
                    {
                        attackUsed = AnimeState.IDLE;
                    }
                    else
                    {
                        attackUsed = AnimeState.InAir;
                    }
                }
                else
                {
                    generateHitBox(attackUsed);
                }
            }

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

        
    }
}

