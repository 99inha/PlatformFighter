/* This file holds the helper functios that the PlayerController class
 * needs.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mechanics
{
    public partial class PlayerController : MonoBehaviour
    {
        /* correctJumpCount:
         *    corrects the jump count and groundedness in case a player walks off stage
         *    without jumping
         *    Args:
         *    Returns:
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
         *     Args: float lagTime - how long the character lags
         *     Returns: IEnumerator - for StartCoroutine
         *     Usage: StartCoroutine("lagForSeconds", lagTime);
         */
        public IEnumerator lagForSeconds(float lagTime)
        {
            //hasControl = false;

            yield return new WaitForSeconds(lagTime);

            hasControl = true;
        }

        /* flip:
         *      flips the transform horizontally
         *      Args:
         *      Returns:
         */
        void flip()
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
            //UnityEngine.Debug.Log("Transform flipped");
        }



        /* startAttack:
         *      Ran when the attack animation starts, it will disable movement and 
         *      add a long lagTime (so player can't buffer attack during attack animation)
         *      Args:
         *      Returns:
         */
        public void startAttack()
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(0, 0);
                hasControl = false;
            }
            lagTime = 3f;  // big attack so players can't buffer an attack during the attack animation

        }


        /* attackLag:
         *      Ran at the end of a attack animation to give the attack some end lag
         *      if the input time is 0, the function will not give control back, it is 
         *      used for attacks that doesn't end when the attack animation time is up
         *      example: attack that can be held
         *      Args: float time - how long the player is lagged after the move
         */
        public void attackLag(float time)
        {
            if (time != 0)
            {
                lagTime = time;

                hasControl = true;
            }

        }

        /* giveControl:
         *      returns control to the player
         *      Args:
         *      Returns:
         */
        public void giveControl()
        {
            hasControl = true;
        }

        /* setVerticalVelocity:
         *      sets the vertical velocity of the player to a set value
         *      overwrittes fallMaxSpeed if necessary
         *      Args: float value - the new vertical velocity of the player
         *      Returns:
         */
        public void setVerticalVelocity(float value)
        {
            rb.velocity = new Vector2(0, value);
            fallMaxSpeed = Mathf.Min(value, fallMaxSpeed);
        }

        /* stablizeVertical:
         *      Once the player reaches maximum fall speed, make the player fall at
         *      uniform velocity
         *      Args:
         *      Returns:
         */
        public void stablizeVertical()
        {
            if (rb.velocity.y < fallMaxSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, fallMaxSpeed);
            }
        }


    }
}

