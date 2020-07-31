using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mechanics
{
    public partial class PlayerController : MonoBehaviour
    {
        public void playerDeath()
        {
            int remainingLives = health.die();
            if (remainingLives != 0)
            {
                respawn();
            }
        }

        void respawn()
        {
            rb.velocity = new Vector2(0, 0);
            teleport(spawnPoint.transform.position);
            health.respawn();
            shield.resetShield();

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
                anime.setAnimator(AnimeState.IsHurt);

                float lagTime = computeLagTime(attack.hasUniformKnockback);
                StartCoroutine("lagForSeconds", lagTime);

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

