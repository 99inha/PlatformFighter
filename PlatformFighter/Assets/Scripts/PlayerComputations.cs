/* This file holds various computations for the player
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
            if (dead)
                return;

            bool isBroken = false;
            if (isGrounded && hasControl && Input.GetButtonDown(ButtonShield))
            {
                holdShield = true;
                hasControl = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
                GameObject.Find(hurtboxName).layer = 9; // change hurtbox to unhittable

            }
            else if (holdShield && Input.GetButtonUp(ButtonShield))
            {
                holdShield = false;
                hasControl = true;
                GameObject.Find(hurtboxName).layer = 10;    // change hurtbox back to hittable

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

        /* computeVulnerStates:
         *      computes the current vulnerability state and transitions from
         *      it to others
         *      Args:
         *      Returns:
         *      Usage: call in Update method
         */
        void computeVulnerStates()
        {
            float horInputAxis = Input.GetAxisRaw(AxisHorizontal);
            float vertInputAxis = Input.GetAxisRaw(AxisVertical);
            bool jumpInput = Input.GetButton(ButtonJump);
            bool AInput = Input.GetButton(ButtonA);
            bool BInput = Input.GetButton(ButtonB);

            bool movementInputted = (horInputAxis != 0) || (vertInputAxis != 0) || jumpInput;
            bool attackInputted = AInput || BInput;

            if (currVulnerState == VulnerState.RESPAWN)
            {
                vulnerStateTimer -= Time.deltaTime;
                if (vulnerStateTimer <= 0)
                {
                    hurtboxObject.layer = 9;
                    vulnerStateTimer = 2f;
                    currVulnerState = VulnerState.UNHITTABLE;
                    Destroy(respawnPlatform);
                }

                if (movementInputted)
                {
                    hurtboxObject.layer = 9;
                    vulnerStateTimer = 2f;
                    currVulnerState = VulnerState.UNHITTABLE;
                    Destroy(respawnPlatform);
                }

                if (attackInputted)
                {
                    hurtboxObject.layer = 10;
                    currVulnerState = VulnerState.HITTABLE;
                    vulnerStateTimer = 2f;
                    Destroy(respawnPlatform);
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
            }

            if (currVulnerState == VulnerState.UNHITTABLE)
            {
                vulnerStateTimer -= Time.deltaTime;
                if (vulnerStateTimer <= 0)
                {
                    gameObject.transform.GetChild(2).gameObject.layer = 10;
                    vulnerStateTimer = 2f;
                    currVulnerState = VulnerState.HITTABLE;
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }

                if (attackInputted)
                {
                    gameObject.transform.GetChild(2).gameObject.layer = 10;
                    currVulnerState = VulnerState.HITTABLE;
                    vulnerStateTimer = 2f;
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }

        void computeHorizontalMovement()
        {
            float inputDirection = Input.GetAxisRaw(AxisHorizontal);

            // if input direction is the opposite side while the user is grounded, flip user
            if (isGrounded && ((inputDirection == 1 && !isFacingRight) ||       // 1 is right and -1 is left
                               (inputDirection == -1 && isFacingRight)))
            {
                flip();
            }
             
            if (inputDirection != 0)
            {
                rb.velocity = new Vector2(moveSpeed * inputDirection, rb.velocity.y);
                horizontalAxisInUse = true;
            }
            else if (horizontalAxisInUse && inputDirection == 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                horizontalAxisInUse = false;
            }
            
        }

        void computeVerticalMovement()
        {
            if (Input.GetButtonDown(ButtonJump) && jumpCount > 0)
            {
                fallMaxSpeed = MAXFALLSPEED;
                isGrounded = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                anime.setAnimator(AnimeState.InAir);
                jumpCount--;
            }
            else if (Input.GetButtonDown(ButtonFall) && (rb.velocity.y < 0.05) && !isGrounded)
            {
                fallMaxSpeed = MAXFASTFALLSPEED;
                rb.velocity = new Vector2(rb.velocity.x, MAXFASTFALLSPEED);
            }

            
        }


    }
}

