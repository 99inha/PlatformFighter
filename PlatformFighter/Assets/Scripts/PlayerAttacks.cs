/* This file holds all methods relating to the player attacking
 * (This includes computeAttacks)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics 
{
    public partial class PlayerController : MonoBehaviour
    {
        void computeAttacks()
        {
            float vertInput = Input.GetAxisRaw("Vertical");  // positive is up and negative is down
            float horInput = Input.GetAxisRaw("Horizontal"); // positive is right and negative is left

            // A button attakcs
            if (Input.GetButtonDown("A"))
            {

                // On ground tilts are computed here
                if (isGrounded)
                {
                    hasControl = false;
                    rb.velocity = new Vector2(0, 0);
                    if (vertInput == 0 && horInput == 0)
                    {
                        jab();
                    }
                    else if (horInput != 0) // no back tilts...either direction procs ftilt
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

                // Aerial attacks are computed here
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


            // Special attacks
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



        // virtual methods for character specific attacks
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

