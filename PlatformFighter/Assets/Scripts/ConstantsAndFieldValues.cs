/* This file stores common constants that are used in multiple scripts and
 * field values of the player controller class.
 * ex) animation states
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public enum AnimeState { 
        IDLE, 
        InAir, 

        NAir,
        FAir,
        BackAir, 
        UpAir,
        DownAir, 

        Jab,
        FTilt,
        UpTilt, 
        DownTilt,
        
        NeutralB, 
        SideB, 
        UpB, 
        DownB, 
        
        ReleaseJab,
        ReleaseNeutralB,
        ReleaseSideB,
        ReleaseUpB,
        ReleaseDownB
    }; // add new states as necessary

    public partial class PlayerController : MonoBehaviour
    {
        public const float MAXFALLSPEED = -12f;
        public const float MAXFASTFALLSPEED = -18F;

        // public fields
        public PlayerHealth health;
        public AnimeState attackHeld;
        public float moveSpeed = 5f;
        public float jumpSpeed = 9f;
        public float fallMaxSpeed = -12f;

        public GameObject shieldObject;

        // only to check the values from Unity Engine
        public Vector2 velocity;
        public Vector3 transformRotation;
        public float horizontalAxis;
        public float verticalAxis;

        // protected fields
        [SerializeField] protected bool hasControl = true;
        [SerializeField] protected bool isGrounded = false;
        protected PlayerAnime anime;
        public float lagTime = 0f;
        protected Rigidbody2D rb;

        // private fields
        Shield shield;

        Vector3 localScale;
        int jumpCount = 1;
        bool holdShield = false;
        bool isDownB = false;
        bool canMove = true;
        bool canAttack = true;
        int upBCount = 1;

        bool isFacingRight = true; // this may need to be changed later on
    }
}
