using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulConstants;

public class Rectangle : PlayerController
{
    protected override void jab()
    {
        UnityEngine.Debug.Log("rectangle is jabbing");
        // logic for jab here
        anime.setAnimator(AnimeState.Jab);
    }

    protected override void fTilt()
    {
        UnityEngine.Debug.Log("rectangle is f tilting");
        anime.setAnimator(AnimeState.FTilt);

        // logic for f tilt here
    }

    protected override void upTilt()
    {
        UnityEngine.Debug.Log("rectangle is up tilting");
        anime.setAnimator(AnimeState.UpTilt);

        // logic for up tilt here
    }

    protected override void downTilt()
    {
        UnityEngine.Debug.Log("rectangle is down tilting");
        anime.setAnimator(AnimeState.DownTilt);

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
}
