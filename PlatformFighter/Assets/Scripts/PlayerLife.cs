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
                GameObject effect = Instantiate(ringOut, transform.position, transform.rotation);
                effect.GetComponent<Ringout>().playRingout(deathZone);
                deathTimer = 1f;
                dead = true;
                hasControl = false;
                // make player "invisible"
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(2).gameObject.layer = 9;
                Debug.Log(gameObject.transform.GetChild(2).gameObject.layer);
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
    }
}

