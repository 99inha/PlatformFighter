using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mechanics
{
    public class Rectangle : PlayerController
    {
        public GameObject shooterObject;

        ProjectileShooter shooter;


        // temp variable for testing
        public LayerMask enemies;


        void Start()
        {
            shooter = shooterObject.GetComponent<ProjectileShooter>();
            base.Start();
        }

        protected override void jab()
        {
            UnityEngine.Debug.Log("rectangle is jabbing");
            anime.setAnimator(AnimeState.Jab);
            animationStart = true;
            attackUsed = AnimeState.Jab;
        }

        protected override void fTilt()
        {
            UnityEngine.Debug.Log("rectangle is f tilting");
            anime.setAnimator(AnimeState.FTilt);

            animationStart = true;
            attackUsed = AnimeState.FTilt;

            // logic for f tilt here
        }

        protected override void upTilt()
        {
            UnityEngine.Debug.Log("rectangle is up tilting");
            anime.setAnimator(AnimeState.UpTilt);

            animationStart = true;
            attackUsed = AnimeState.UpTilt;
            // logic for up tilt here
        }

        protected override void downTilt()
        {
            UnityEngine.Debug.Log("rectangle is down tilting");
            anime.setAnimator(AnimeState.DownTilt);
            animationStart = true;
            attackUsed = AnimeState.DownTilt;
            // logic for down tilt here
        }

        protected override void nair()
        {
            UnityEngine.Debug.Log("rectangle is nair-ing");
            anime.setAnimator(AnimeState.NAir);

            // logic for nair here
        }

        protected override void fair()
        {
            UnityEngine.Debug.Log("rectangle is fair-ing");
            anime.setAnimator(AnimeState.FAir);
            animationStart = true;
            attackUsed = AnimeState.FAir;
            // logic for fair here
        }

        protected override void bair()
        {
            UnityEngine.Debug.Log("rectangle is bair-ing");
            anime.setAnimator(AnimeState.BackAir);

            // logic for bair here
        }

        protected override void upair()
        {
            UnityEngine.Debug.Log("rectangle is up air-ing");
            anime.setAnimator(AnimeState.UpAir);

            // logic for up air here
        }

        protected override void downair()
        {
            UnityEngine.Debug.Log("rectangle is down air-ing");
            anime.setAnimator(AnimeState.DownAir);

            // logic for down air here
        }

        protected override void neutralB()
        {
            UnityEngine.Debug.Log("rectangle is neutral-b-ing");
            anime.setAnimator(AnimeState.NeutralB);
            hasControl = false;
            attackHeld = AnimeState.NeutralB;
        }

        protected override void upB()
        {
            UnityEngine.Debug.Log("rectangle is up b-ing");
            anime.setAnimator(AnimeState.UpB);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(0, rb.velocity.y - 8));
        }

        protected override void sideB()
        {
            UnityEngine.Debug.Log("rectangle is side b-ing");
            anime.setAnimator(AnimeState.SideB);
            animationStart = true;
            attackUsed = AnimeState.SideB;
        }

        protected override void downB()
        {
            UnityEngine.Debug.Log("rectangle is down b-ing");
            anime.setAnimator(AnimeState.DownB);
            if (!isGrounded)
            {
                fallMaxSpeed = -2f;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(-2f, rb.velocity.y));
            }

            attackHeld = AnimeState.DownB;
        }

        protected override void releaseJab()
        {
            attackHeld = AnimeState.IDLE;
            anime.setAnimator(AnimeState.ReleaseJab);

            hasControl = true;
        }

        protected override void releaseNeutralB()
        {
            attackHeld = AnimeState.IDLE;
            anime.setAnimator(AnimeState.ReleaseNeutralB);

            hasControl = true;
        }

        protected override void releaseDownB()
        {
            attackHeld = AnimeState.IDLE;
            anime.setAnimator(AnimeState.ReleaseDownB);
            fallMaxSpeed = -12f;
        
            hasControl = true;
        }

        /* shootBullet:
         *      a function called by an animation to shoot a bullet From the Rectangle's mouth
         *      Args:
         *      Returns:
         */
        void shootBullet()
        {
            shooter.shoot();
        }

        protected override void generateHitBox(AnimeState attack)
        {
            Collider2D[] colliders = { };
            float damage = 0f;
            Vector2 hitDirection = new Vector2(0,0);

            if (attack == AnimeState.Jab)
            {
                damage = 0f;
                hitDirection.x = 1.2f;
                hitDirection.y = 0.9f;


                Vector3 v = transform.TransformPoint(0.7f, -0.15f, transform.position.z);
                colliders = Physics2D.OverlapBoxAll(new Vector2(v.x, v.y), transform.TransformVector(0.5f, 0.5f, 1), 0, enemies);


                if (!isFacingRight)
                {
                    hitDirection.x = hitDirection.x * -1;
                }

            }
            else if (attack == AnimeState.FTilt)
            {
                damage = 0f;
                hitDirection.x = 7f;
                hitDirection.y = 1f;
                if (isFacingRight)
                {
                    colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 0.8f, transform.position.y + 0.1f), new Vector2(1, 0.9f), 0, enemies);
                }
                else
                {
                    hitDirection.x = hitDirection.x * -1;
                    colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 0.8f, transform.position.y + 0.1f), new Vector2(1, 0.9f), 0, enemies);
                }
            }
            else if (attack == AnimeState.UpTilt)
            {
                damage = 0f;
                hitDirection.x = -0.5f;
                hitDirection.y = 8f;

                if (!isFacingRight)
                {
                    hitDirection.x = -1 * hitDirection.x;
                }

                colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 0.5f), new Vector2(2.3f, 1), 0, enemies);

            }
            else if (attack == AnimeState.DownTilt)
            {
                damage = 0f;
                hitDirection.x = 0.5f;
                hitDirection.y = 8f;
                if (isFacingRight)
                {
                    colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 0.8f, transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0, enemies);
                }
                else
                {
                    hitDirection.x = hitDirection.x * -1;
                    colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 0.8f, transform.position.y - 0.5f), new Vector2(0.9f, 0.4f), 0, enemies);
                }
            }
            else if (attack == AnimeState.NAir)
            {

            }
            else if (attack == AnimeState.FAir)
            {
                damage = 0f;
                hitDirection.x = 0;
                hitDirection.y = -20f;
                if (isFacingRight)
                {
                    colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + 0.3f, transform.position.y + 0.4f), 0.6f, enemies);

                }
                else
                {
                    hitDirection.x = hitDirection.x * -1;
                    colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x - 0.3f, transform.position.y + 0.4f), 0.6f, enemies);

                }
            }
            else if (attack == AnimeState.BackAir)
            {

            }
            else if (attack == AnimeState.UpAir)
            {

            }
            else if (attack == AnimeState.DownAir)
            {

            }
            else if (attack == AnimeState.NeutralB)
            {

            }
            else if (attack == AnimeState.UpB)
            {
                
            }
            else if (attack == AnimeState.SideB)
            {
                damage = 0f;
                hitDirection.x = 6f;
                hitDirection.y = 1f;
                if (isFacingRight)
                {
                    colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x + 1.1f, transform.position.y + 0.1f), new Vector2(2.2f, 0.4f), 0, enemies);
                }
                else
                {
                    hitDirection.x = hitDirection.x * -1;
                    colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x - 1.1f, transform.position.y + 0.1f), new Vector2(2.2f, 0.4f), 0, enemies);
                }
            }
            else if (attack == AnimeState.DownB)
            {

            }

            string name;
            foreach (Collider2D d in colliders)
            {
                //Debug.Log(d.name);
                name = d.name;

                if (!hasCollided(name))
                {
                    d.GetComponentInParent<MovementTest>().takeDamage(damage, hitDirection);
                    collided.Add(name);
                    Debug.Log(d.name);

                }
            }


        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(255, 0, 0, 0.5f);

            //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

            // Jab
            //Gizmos.DrawCube(new Vector3(transform.position.x + 0.7f, transform.position.y - 0.15f, transform.position.z), new Vector3(0.5f, 0.5f, 1));
            Gizmos.DrawCube(transform.TransformPoint(0.7f, -0.15f, transform.position.z), transform.TransformVector(0.5f, 0.5f, 1));

            // FAir
            // Gizmos.DrawSphere(new Vector2(transform.position.x + 0.3f, transform.position.y + 0.4f), 0.6f);
            // Gizmos.DrawSphere(new Vector2(transform.position.x - 0.3f, transform.position.y + 0.4f), 0.6f);

            // FTilt
            // Gizmos.DrawCube(new Vector3(transform.position.x + 0.8f, transform.position.y + 0.1f, transform.position.z), new Vector3(1, 0.9f, 1));
            // Gizmos.DrawCube(new Vector3(transform.position.x - 0.8f, transform.position.y + 0.1f, transform.position.z), new Vector3(1, 0.9f, 1));

            // DownTilt
            // Gizmos.DrawCube(new Vector3(transform.position.x + 0.8f, transform.position.y - 0.5f, transform.position.z), new Vector3(0.9f, 0.4f, 1));
            // Gizmos.DrawCube(new Vector3(transform.position.x - 0.8f, transform.position.y - 0.5f, transform.position.z), new Vector3(0.9f, 0.4f, 1));

            // UpTilt
            // Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(2.3f,1, 1));

            // SideB
            //Gizmos.DrawCube(new Vector3(transform.position.x + 1.1f, transform.position.y + 0.1f, transform.position.z), new Vector3(2.2f,0.4f, 1));

        }
    }
}

