using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulConstants;

public class PlayerAnime : MonoBehaviour
{

    float transformRotateAngle = 0f;
    Animator anime;

    
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(0, 0, transformRotateAngle * transform.localScale.x);
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

    void setUpB()
    {
        anime.SetTrigger("UpB");
    }


    // animation helper functions
    void transformRotate(float angle)
    {
        //UnityEngine.Debug.Log("Rotation happening by anlge: " + angle * transform.localScale.x);
        transformRotateAngle = angle;
    }
}
