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

    /* Attack struct:
     *      a struct that carries the information of the attack 
     *      from the user to the receiver
     *      
     *      fields:
     *          -float damage = how much damage the attack does
     *          -Vector2 knockback = the direction and the magnitude of
     *                               the knockbakc of the attack
     *          -bool hasUniformKnockback = whether or not the knockback is
     *                                      uniform
     */
    public struct Attack
    {
        public float damage { get; }
        public Vector2 knockback { get; }
        public bool hasUniformKnockback { get; }

        public Attack(float damage, Vector2 knockback, bool knockbackType)
        {
            this.damage = damage;
            this.knockback = knockback;
            this.hasUniformKnockback = knockbackType;
        }

        
    }



    public partial class PlayerController : MonoBehaviour
    {
        public const float MAXFALLSPEED = -12f;
        public const float MAXFASTFALLSPEED = -18F;

        // public fields
        public int playerNumber;
        public PlayerHealth health;
        public AnimeState attackHeld; // State tracker for what attack is being held currently
                                      // needs to be updated by the child whenever a holdable attack
                                      // is pressed or released
        public float moveSpeed = 5f;
        public float jumpSpeed = 9f;
        public float fallMaxSpeed = -12f;

        public GameObject shieldObject;

        // only to check the values from Unity Engine
        public Vector2 velocity;
        public Vector3 transformRotation;
        public float horizontalAxis;
        public float verticalAxis;
        public Vector3 right;

        // protected fields
        [SerializeField] protected bool hasControl = true;
        [SerializeField] protected bool isGrounded = false;
        protected PlayerAnime anime;
        public float lagTime = 0f;
        protected Rigidbody2D rb;
        protected string hurtboxName;
        protected bool animationStart = false;
        protected float animationTime = 0f;
        protected AnimeState attackUsed;
        protected List<string> collided;
        protected bool isFacingRight = true; // this may need to be changed later on

        // private fields
        Shield shield;

        bool hitboxGen = false;
        Vector3 localScale;
        int jumpCount = 1;
        bool holdShield = false;
        bool canMove = true;
        bool canAttack = true;
        int upBCount = 1;
        bool horizontalAxisInUse = false;

        // Input buttons
        string AxisHorizontal;
        string AxisVertical;
        string ButtonA;
        string ButtonB;
        string ButtonJump;
        string ButtonFall;
        string ButtonShield;

    }
}
