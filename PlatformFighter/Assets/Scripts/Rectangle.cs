using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : PlayerController
{
    protected override void jab()
    {
        UnityEngine.Debug.Log("rectangle is jabbing");
        anime.SetTrigger("NAttack");
        // logic for jab here
    }

    protected override void fTilt()
    {
        UnityEngine.Debug.Log("rectangle is f tilting");
        anime.SetTrigger("FAttack");

        // logic for f tilt here
    }

    protected override void upTilt()
    {
        UnityEngine.Debug.Log("rectangle is up tilting");
        anime.SetTrigger("UAttack");

        // logic for up tilt here
    }

    protected override void downTilt()
    {
        UnityEngine.Debug.Log("rectangle is down tilting");
        anime.SetTrigger("DAttack");

        // logic for down tilt here
    }

    protected override void nair()
    {
        UnityEngine.Debug.Log("rectangle is nair-ing");
        anime.SetTrigger("NAttack");

        // logic for nair here
    }

    protected override void fair()
    {
        UnityEngine.Debug.Log("rectangle is fair-ing");
        anime.SetTrigger("FAttack");

        // logic for fair here
    }

    protected override void bair()
    {
        UnityEngine.Debug.Log("rectangle is bair-ing");
        anime.SetTrigger("BAttack");

        // logic for bair here
    }

    protected override void upair()
    {
        UnityEngine.Debug.Log("rectangle is up air-ing");
        anime.SetTrigger("UAttack");

        // logic for up air here
    }

    protected override void downair()
    {
        UnityEngine.Debug.Log("rectangle is down air-ing");
        anime.SetTrigger("DAttack");

        // logic for down air here
    }
}
