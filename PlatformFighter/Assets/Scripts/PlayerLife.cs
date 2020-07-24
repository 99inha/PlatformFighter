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
    }
}

