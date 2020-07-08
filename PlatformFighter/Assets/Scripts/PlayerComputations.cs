﻿/* This file holds various computations for the player
 * (This file does not hold the computation for attack)
 * (For computeAttack see PlayerAttacks.cs)
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public partial class PlayerController : MonoBehaviour
    {

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

            
        }


    }
}
