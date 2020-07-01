using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulConstants;

public class PlayerAnime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* setAnimator:
     *   manipulates the animator parameters to best fit the current state
     *      Args: AnimeState state: state to transition the animator to
     *      Returns: Nothing
     */
    public void setAnimator(AnimeState state)
    {
        // switch case here for the states; call the methods below
    }


    // separating attacks for cleaner code

    void setJab()
    {
        // animation logic for jab here
    }

    void setFTilt()
    {
        // animation logic for f tilt here
    }

    void setUpTilt()
    {
        // animation logic for up tilt here
    }

    void setDownTilt()
    {
        // animation logic for down tilt here
    }

    void setNair()
    {
        // animation logic for nair here
    }

    void setFair()
    {
        // animation logic for fair here
    }

    void setBair()
    {
        // animation logic for bair here
    }

    void setUpair()
    {
        // animation logic for up air here
    }

    void setDownair()
    {
        // animation logic for down air here
    }
}
