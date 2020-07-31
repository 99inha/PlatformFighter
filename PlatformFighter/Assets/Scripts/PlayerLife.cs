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
                GameObject effect = Instantiate(ringOut, transform.position, transform.rotation);
                effect.GetComponent<Ringout>().playRingout(rb.position.x, rb.position.y);
                deathTimer = 2f;
                dead = true;
                hasControl = false;

            }
        }

        

        void respawn()
        {
            hasControl = true;
            rb.velocity = new Vector2(0, 0);
            teleport(spawnPoint.transform.position);
            health.respawn();
            shield.resetShield();

        }
    }
}

