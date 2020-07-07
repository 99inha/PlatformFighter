using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulConstants;

public class PlayerAnime : MonoBehaviour
{

    Animator anime;
    public Vector3 transformAngles;

    
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //only to check its values from Unity Editor
        transformAngles = transform.eulerAngles;
    }

    /* setAnimator:
     *   manipulates the animator parameters to best fit the current state
     *      Args: AnimeState state: state to transition the animator to
     *      Returns: Nothing
     */
    public void setAnimator(AnimeState state)
    {
        // switch case here for the states; call the methods below

        switch (state)
        {
            case AnimeState.IDLE:
                anime.SetBool("IsGrounded", true);
                anime.SetTrigger("TouchGround");
                break;
            case AnimeState.InAir:
                anime.SetBool("IsGrounded", false);
                break;
            case AnimeState.DownAir:
                setDownair();
                break;
            case AnimeState.UpAir:
                setUpair();
                break;
            case AnimeState.FAir:
                setFair();
                break;
            case AnimeState.BackAir:
                setBair();
                break;
            case AnimeState.NAir:
                setNair();
                break;
            case AnimeState.DownTilt:
                setDownTilt();
                break;
            case AnimeState.UpTilt:
                setUpTilt();
                break;
            case AnimeState.Jab:
                setJab();
                break;
            case AnimeState.FTilt:
                setFTilt();
                break;
            case AnimeState.UpB:
                setUpB();
                break;
            case AnimeState.SideB:
                setSideB();
                break;
            case AnimeState.DownB:
                setDownB();
                break;
            case AnimeState.ReleaseB:
                releaseDownB();
                break;
            case AnimeState.NeutralB:
                setNeutralB();
                break;
        }
    }


    // separating attacks for cleaner code

    void setJab()
    {
        // animation logic for jab here
        anime.SetTrigger("NAttack");

    }

    void setFTilt()
    {
        // animation logic for f tilt here
        anime.SetTrigger("FAttack");
    }

    void setUpTilt()
    {
        // animation logic for up tilt here
        anime.SetTrigger("UAttack");
    }

    void setDownTilt()
    {
        // animation logic for down tilt here
        anime.SetTrigger("DAttack");
    }

    void setNair()
    {
        // animation logic for nair here
        anime.SetTrigger("NAttack");
    }

    void setFair()
    {
        // animation logic for fair here
        anime.SetTrigger("FAttack");
    }

    void setBair()
    {
        // animation logic for bair here
        anime.SetTrigger("BAttack");
    }

    void setUpair()
    {
        // animation logic for up air here
        anime.SetTrigger("UAttack");
    }

    void setDownair()
    {
        // animation logic for down air here
        anime.SetTrigger("DAttack");
    }

    void setNeutralB()
    {

        anime.SetTrigger("NeutralB");
    }

    void setUpB()
    {
        anime.SetTrigger("UpB");
    }

    void setSideB()
    {
        anime.SetTrigger("SideB");
    }


    void setDownB()
    {
        anime.SetBool("DownB", true);

    }

    void releaseDownB()
    {
        anime.SetBool("DownB", false) ;
    }

    // animation helper functions
    /* transformRotate:
     *      rotates the transform by the given angle from the CURRENT angle
     *      Args: float angle - angle to turn by in the z axis
     *      Usage: call this function from an animation as an event.
     *              -negative values rotate the transform FORWARD
     *              -positive values rotate the transform BACKWARD
     *              -0 will reset the transform to 0
     *      ***NOTE: -when doing transform rotations in animations,
     *               DO NOT USE THE TRANSFORM ROTATE PROPERTY
     *               Animation engine will forbid any code from changing
     *               the rotations, and will cause all rotations caused by code
     *               to be WACK
     *               
     */
    void transformRotate(float angle)
    {
        //UnityEngine.Debug.Log("Rotation happening by anlge: " + angle * transform.localScale.x);
        if (angle == 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y, 0f)); 
        }

        transform.Rotate(0, 0, angle);
    }
}
