using System;
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

            if (!dead)
            {
                GameObject effect = Instantiate(ringOut, transform.position, new Quaternion(0,0,0,0));
                Debug.Log(transform.rotation);
                effect.GetComponent<Ringout>().playRingout(deathZone);
            }

            deathTimer = 1f;
            dead = true;
            hasControl = false;
            // make player "invisible"
            spriteRenderer.enabled = false;
            hurtboxObject.layer = 9;

            anime.setAnimator(AnimeState.Dead);
            //Debug.Log(gameObject.transform.GetChild(2).gameObject.layer);


            if(remainingLives == 0)
            {
                lostGame = true; // so the player don't respawn
            }
        }

        void respawn()
        {
            spriteRenderer.enabled = true;

            // make character unhittable by changing layers
            hurtboxObject.layer = 11;
            currVulnerState = VulnerState.RESPAWN;
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);

            // spawn a respawn platform
            respawnPlatform = Instantiate(
                respawnPlatformPrefab,
                spawnPoint.transform.position,
                spawnPoint.transform.rotation
            );

            // respawn the character at spawn point
            rb.velocity = new Vector2(0, 0);
            teleport(spawnPoint.transform.position);

            health.respawn();
            shield.resetShield();
            hasControl = true;
            holdShield = false;
        }

        /* hurtLag:
         *     strips the control over the character for a given amount of seconds
         *     and enters the hurt state
         *     Args: float lagTime - how long the character lags
         *     Returns: IEnumerator - for StartCoroutine
         *     Usage: StartCoroutine("lagForSeconds", lagTime);
         *     *** this function is outdated by a lag time computation that happens at update
         */
        public IEnumerator hurtLag(float lagTime)
        {
            hasControl = false;
            anime.setAnimator(AnimeState.IsHurt);
            isHurt = true;

            yield return new WaitForSeconds(lagTime);

            isHurt = true;
            anime.setAnimator(AnimeState.ExitHurt);
            hasControl = true;
        }

        public void takeDamage(Attack attack, Vector3 location)
        {
            if (holdShield)
            {   // Damage is applied on the shield
                shield.takeDamage(attack);
            }
            else
            {   // Damage is applied on the player
                //StartCoroutine("lagForSeconds", 1f);

                // generate hit effect

                Instantiate(hitEffect, findMidpoint(location, transform.position), new Quaternion(0, 0, 0, 0));

                Vector2 finalKnockback = health.takeDamage(attack);

                hurtLagTime = computeLagTime(attack.hasUniformKnockback);
                //StartCoroutine("hurtLag", hurtLagTime);

                hasControl = false;
                anime.setAnimator(AnimeState.IsHurt);
                isHurt = true;
                rb.velocity = finalKnockback;
                //UnityEngine.Debug.Log(rb.velocity.x);


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


        // findDistance: finds the 2d distance between 2 Vector3 points, the z coordinate is not used
        Vector3 findMidpoint(Vector3 p1, Vector3 p2)
        {
            float xPos = (p1.x + p2.x) / 2;
            float yPos = (p1.y + p2.y) / 2;
            return new Vector3(xPos, yPos, 0);
        }
    }


}

