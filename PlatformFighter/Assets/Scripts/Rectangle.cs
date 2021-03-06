﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            // for rectangle to have a different lag time than default, change it here
        }

        protected override void jab()
        {
            //UnityEngine.Debug.Log("rectangle is jabbing");
            anime.setAnimator(AnimeState.Jab);
            animationStart = true;
            canContinueAttack = true;
            attackUsed = AnimeState.Jab;
        }

        protected override void fTilt()
        {
            //UnityEngine.Debug.Log("rectangle is f tilting");
            anime.setAnimator(AnimeState.FTilt);

            animationStart = true;
            attackUsed = AnimeState.FTilt;

            // logic for f tilt here
        }

        protected override void upTilt()
        {
            //UnityEngine.Debug.Log("rectangle is up tilting");
            anime.setAnimator(AnimeState.UpTilt);

            animationStart = true;
            attackUsed = AnimeState.UpTilt;
            // logic for up tilt here
        }

        protected override void downTilt()
        {
            //UnityEngine.Debug.Log("rectangle is down tilting");
            anime.setAnimator(AnimeState.DownTilt);
            animationStart = true;
            attackUsed = AnimeState.DownTilt;
            // logic for down tilt here
        }

        protected override void nair()
        {
            //UnityEngine.Debug.Log("rectangle is nair-ing");
            anime.setAnimator(AnimeState.NAir);
            animationStart = true;
            attackUsed = AnimeState.NAir;

            // logic for nair here
        }

        protected override void fair()
        {
            //UnityEngine.Debug.Log("rectangle is fair-ing");
            anime.setAnimator(AnimeState.FAir);
            animationStart = true;
            attackUsed = AnimeState.FAir;
            // logic for fair here
        }

        protected override void bair()
        {
            //UnityEngine.Debug.Log("rectangle is bair-ing");
            anime.setAnimator(AnimeState.BackAir);
            animationStart = true;
            attackUsed = AnimeState.BackAir;

            // logic for bair here
        }

        protected override void upair()
        {
            //UnityEngine.Debug.Log("rectangle is up air-ing");
            anime.setAnimator(AnimeState.UpAir);
            animationStart = true;
            attackUsed = AnimeState.UpAir;

            // logic for up air here
        }

        protected override void downair()
        {
            //UnityEngine.Debug.Log("rectangle is down air-ing");
            anime.setAnimator(AnimeState.DownAir);
            animationStart = true;
            attackUsed = AnimeState.DownAir;

            // logic for down air here
        }

        protected override void neutralB()
        {
            //UnityEngine.Debug.Log("rectangle is neutral-b-ing");
            anime.setAnimator(AnimeState.NeutralB);
            hasControl = false;
            canContinueAttack = true;
            attackHeld = AnimeState.NeutralB;
        }

        protected override void upB()
        {
            //UnityEngine.Debug.Log("rectangle is up b-ing");
            anime.setAnimator(AnimeState.UpB);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(0, rb.velocity.y - 8));
        }

        protected override void sideB()
        {
            //UnityEngine.Debug.Log("rectangle is side b-ing");
            anime.setAnimator(AnimeState.SideB);
            animationStart = true;
            attackUsed = AnimeState.SideB;
        }

        protected override void downB()
        {
            // UnityEngine.Debug.Log("rectangle is down b-ing");
            anime.setAnimator(AnimeState.DownB);
            if (!isGrounded)
            {
                fallMaxSpeed = -2f;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(-2f, rb.velocity.y));
            }
            canContinueAttack = false;
            animationStart = true;
            attackUsed = AnimeState.DownB;

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
            bool hasUniformKnockback = false;
            bool reflect = false;
            Vector3 hitLocation = new Vector3(0, 0, 0);
            

            // positive x values for away and negative for inwards
            // positive y values for up and negative values for down
            if (attack == AnimeState.Jab)
            {
                damage = 1.2f;
                hitDirection.x = 1.5f;
                hitDirection.y = 0.9f;
                hasUniformKnockback = true;
                hitLocation = new Vector2(transform.position.x + 0.7f * transform.right.x, transform.position.y);

                //Vector3 v = transform.TransformPoint(0.7f, -0.15f, transform.position.z);
                colliders = Physics2D.OverlapBoxAll(
                    (Vector2)hitLocation,
                    new Vector2(0.5f, 0.5f), 0, enemies);

            }
            else if (attack == AnimeState.FTilt)
            {
                damage = 7f;
                hitDirection.x = 6f;
                hitDirection.y = 1.5f;
                hitLocation = new Vector2(transform.position.x + 0.8f * transform.right.x, transform.position.y + 0.1f);

                colliders = Physics2D.OverlapBoxAll(
                    (Vector2)hitLocation,
                    new Vector2(1, 0.9f), 0, enemies);

            }
            else if (attack == AnimeState.UpTilt)
            {
                damage = 4f;
                hitDirection.x = -0.5f;
                hitDirection.y = 5f;
                hitLocation = new Vector2(transform.position.x, transform.position.y + 0.5f);

                colliders = Physics2D.OverlapBoxAll(
                    (Vector2)hitLocation,
                    new Vector2(2.3f, 1), 0, enemies);

            }
            else if (attack == AnimeState.DownTilt)
            {
                damage = 5f;
                hitDirection.x = 0.5f;
                hitDirection.y = 10f;
                hitLocation = new Vector2(transform.position.x + 0.8f * transform.right.x, transform.position.y - 0.5f);

                colliders = Physics2D.OverlapBoxAll(
                    (Vector2)hitLocation,
                    new Vector2(0.9f, 0.4f), 0, enemies);  

            }
            else if (attack == AnimeState.NAir)
            {
                damage = 3f;
                hitDirection.x = 3f;
                hitDirection.y = 3f;
                hitLocation = transform.position;
                colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 0.72f, enemies);
            }
            else if (attack == AnimeState.FAir)
            {
                damage = 6.6f;
                hitDirection.x = 0.7f;
                hitDirection.y = -20f;
                hitLocation = new Vector2(transform.position.x + transform.right.x * 0.3f,transform.position.y + 0.4f);

                colliders = Physics2D.OverlapCircleAll(
                    (Vector2)hitLocation, 
                    0.6f, 
                    enemies);
            }
            else if (attack == AnimeState.BackAir)
            {
                damage = 0f;
                hitDirection.x = 8f;
                hitDirection.y = 1f;

                Vector3 t = transform.TransformPoint(-0.025f, -0.6f, transform.position.z);
                hitLocation = new Vector2(t.x, t.y);


                colliders = Physics2D.OverlapBoxAll((Vector2)hitLocation, transform.TransformVector(0.9f, 0.7f, 1f), 0, enemies);
            }
            else if (attack == AnimeState.UpAir)
            {
                damage = 3.2f;
                hitDirection.x = 1f;
                hitDirection.y = 5f;

                Vector3 t = transform.TransformPoint(0f, 0.9f, transform.position.z);
                hitLocation = new Vector2(t.x, t.y);

                colliders = Physics2D.OverlapBoxAll((Vector2)hitLocation, transform.TransformVector(0.5f, 1.2f, 1f), 0, enemies);
            }
            else if (attack == AnimeState.DownAir)
            {
                damage = 9f;
                hitDirection.x = 5f;
                hitDirection.y = 3f;

                Vector3 t = transform.TransformPoint(-0.032f, 0.032f, transform.position.z);
                hitLocation = new Vector2(t.x, t.y);

                colliders = Physics2D.OverlapBoxAll((Vector2)hitLocation, new Vector2(1.188f, 1.188f), 0, enemies);
            }
            else if (attack == AnimeState.NeutralB)
            {

            }
            else if (attack == AnimeState.UpB)
            {
                
            }
            else if (attack == AnimeState.SideB)
            {
                damage = 4f;
                hitDirection.x = 6f;
                hitDirection.y = 1f;
                Vector3 t = new Vector3(transform.position.x + 1.1f * transform.right.x, transform.position.y + 0.1f, transform.position.z);
                hitLocation = new Vector2(t.x, t.y);


                colliders = Physics2D.OverlapBoxAll(
                    (Vector2)hitLocation,
                    new Vector2(2.2f, 0.4f), 0, enemies);

            }
            else if (attack == AnimeState.DownB)
            {
                damage = 2f;
                hitDirection.x = 3f;
                hitDirection.y = 1f;

                hitLocation = transform.position;
                colliders = Physics2D.OverlapBoxAll(
                    new Vector2(transform.position.x - 0.035f * transform.right.x, transform.position.y + 0.05f),
                    new Vector2(1.8f, 1.7f), 0, enemies);
                reflect = true;
            }

            string name;
            foreach (Collider2D d in colliders)
            {
                //Debug.Log(d.name);
                name = d.name;

                if (!hasCollided(name) && (string.Compare(name, hurtboxName) != 0))
                {
                    if(d.gameObject.layer == 10)
                    {   // if hit box hit another player

                        // calculating the direction of the hit
                        float directionX = d.transform.position.x - transform.position.x;
                        hitDirection.x *= directionX / Mathf.Abs(directionX); // away from the player
                        Attack attackStruct = new Attack(damage, hitDirection, hasUniformKnockback);


                        d.GetComponentInParent<PlayerController>().takeDamage(attackStruct, hitLocation);

                    }
                    else if(d.gameObject.layer == 8 )
                    {   // if hit box hit a bullet
                        if (reflect && d.gameObject.tag != transform.tag)
                        {   // if we used DownB and reflected the bullet
                            Vector2 velo = d.gameObject.GetComponent<Rigidbody2D>().velocity;
                            velo.x = velo.x * -1 * 1.1f;   // bullet gets faster when reflected
                            d.gameObject.GetComponent<Rigidbody2D>().velocity = velo;
                            d.gameObject.GetComponent<RectBullet>().lifeTime = 1f;
                            d.gameObject.tag = transform.tag;

                        }

                    }

                    collided.Add(name);
                    Debug.Log(d.name + "asdfasdfasdf:" + collided[0]);
                }
                
            }


        }


        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(255, 0, 0, 0.5f);

            //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

            // NAir
            // Gizmos.DrawSphere(new Vector2(transform.position.x, transform.position.y), 0.72f);

            // FAir
            // Gizmos.DrawSphere(new Vector2(transform.position.x + transform.right.x * 0.3f, transform.position.y + 0.4f), 0.6f);

            // BAir
            /* Gizmos.DrawCube(
                transform.TransformPoint(
                    -0.025f,
                    -0.6f,
                    transform.localPosition.z),
                transform.TransformVector(0.9f, 0.7f, 1)); */

            // UpAir
            /* Gizmos.DrawCube(
                transform.TransformPoint(
                    0f,
                    0.9f,
                    transform.localPosition.z),
                transform.TransformVector(0.5f, 1.2f, 1)); */

            // DownAir
            /*
            Gizmos.DrawCube(
                transform.TransformPoint(
                    -0.032f,
                    0.032f,
                    transform.localPosition.z),
                transform.TransformVector(1.188f, 1.188f, 1f));

            Gizmos.color = new Color(0, 255, 0, 0.5f);
            */

            // Jab
            //Gizmos.DrawCube(new Vector3(transform.position.x + 0.7f, transform.position.y - 0.15f, transform.position.z), new Vector3(0.5f, 0.5f, 1));
            /*Gizmos.DrawCube(
                transform.TransformPoint(0.7f, -0.15f, transform.position.z), 
                transform.TransformVector(0.5f, 0.5f, 1));
            */

            // FTilt
            /*Gizmos.DrawCube(
                transform.TransformPoint(0.8f, 0.1f, transform.position.z), 
                transform.TransformVector(1f, 0.9f, 1));
            */

            // DownTilt
            // Gizmos.DrawCube(transform.TransformPoint(0.8f, -0.5f, transform.position.z), transform.TransformVector(0.9f, 0.4f, 1));



            // UpTilt
            //Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), new Vector3(2.3f,1, 1));

            // SideB
            //Gizmos.DrawCube(new Vector3(transform.position.x + 1.1f, transform.position.y + 0.1f, transform.position.z), new Vector3(2.2f,0.4f, 1));
            //Gizmos.DrawCube(transform.TransformPoint(1.1f, 0.1f, transform.position.z), transform.TransformVector(2.2f, 0.4f, 1));

            // DownB
            // Gizmos.DrawCube(new Vector3(transform.position.x - 0.035f, transform.position.y + 0.05f, transform.position.z), new Vector3(2f,1.7f, 1));


        }
    }
}

