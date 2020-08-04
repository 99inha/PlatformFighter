using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;


namespace Mechanics
{
    public partial class PlayerController : MonoBehaviour
    {
        public void playerDeath(int deathZone)
        {
            int remainingLives = health.die();
            if (remainingLives != 0)
            {
                if (!dead)
                {
                    GameObject effect = Instantiate(ringOut, transform.position, transform.rotation);
                    effect.GetComponent<Ringout>().playRingout(deathZone);
                }

                deathTimer = 1f;
                dead = true;
                hasControl = false;
                // make player "invisible"
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(2).gameObject.layer = 9;
                //Debug.Log(gameObject.transform.GetChild(2).gameObject.layer);
            }
        }

        

        void respawn()
        {
            gameObject.transform.GetChild(2).gameObject.layer = 10;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            hasControl = true;
            holdShield = false;
            rb.velocity = new Vector2(0, 0);
            teleport(spawnPoint.transform.position);
            health.respawn();
            shield.resetShield();

        }

        /* hurtLag:
         *     strips the control over the character for a given amount of seconds
         *     and enters the hurt state
         *     Args: float lagTime - how long the character lags
         *     Returns: IEnumerator - for StartCoroutine
         *     Usage: StartCoroutine("lagForSeconds", lagTime);
         */
        public IEnumerator hurtLag(float lagTime)
        {
            hasControl = false;
            anime.setAnimator(AnimeState.IsHurt);

            yield return new WaitForSeconds(lagTime);

            anime.setAnimator(AnimeState.ExitHurt);
            hasControl = true;
        }

        public void takeDamage(Attack attack)
        {
            if (holdShield)
            {   // Damage is applied on the shield
                shield.takeDamage(attack);
            }
            else
            {   // Damage is applied on the player
                //StartCoroutine("lagForSeconds", 1f);
                Vector2 finalKnockback = health.takeDamage(attack);

                float lagTime = computeLagTime(attack.hasUniformKnockback);
                StartCoroutine("hurtLag", lagTime);

                rb.velocity = finalKnockback;
                UnityEngine.Debug.Log(rb.velocity.x);


                // Apply damage to playerHealth

            }
        }

        public float computeLagTime(bool attackUniformLag)
        {
            if (attackUniformLag)
            {
                return hitStunTime;
            } else
            {
                float currHealth = health.getHealth();
                if (currHealth < 30f)
                {
                    return hitStunTime;
                } else if (currHealth < 60f)
                {
                    return hitStunTime * 2;
                } else if (currHealth < 90f)
                {
                    return hitStunTime * 3;
                } else if (currHealth < 120f)
                {
                    return hitStunTime * 4;
                } else
                {
                    return hitStunTime * 5;
                }
            }
        }

        protected virtual void generateHitBox(AnimeState attack)
        {

        }
    }


}

