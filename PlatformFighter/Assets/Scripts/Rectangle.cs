using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : PlayerController
{
    protected override void jab()
    {
        UnityEngine.Debug.Log("rectangle is jabbing");

        // logic for jab here
    }

    protected override void fTilt()
    {
        UnityEngine.Debug.Log("rectangle is f tilting");

        // logic for f tilt here
    }

    protected override void upTilt()
    {
        UnityEngine.Debug.Log("rectangle is up tilting");

        // logic for up tilt here
    }

    protected override void downTilt()
    {
        UnityEngine.Debug.Log("rectangle is down tilting");

        // logic for down tilt here
    }

    protected override void nair()
    {
        UnityEngine.Debug.Log("rectangle is nair-ing");

        // logic for nair here
    }

    protected override void fair()
    {
        UnityEngine.Debug.Log("rectangle is fair-ing");

        // logic for fair here
    }

    protected override void bair()
    {
        UnityEngine.Debug.Log("rectangle is bair-ing");

        // logic for bair here
    }

    protected override void upair()
    {
        UnityEngine.Debug.Log("rectangle is up air-ing");

        // logic for up air here
    }

    protected override void downair()
    {
        UnityEngine.Debug.Log("rectangle is down air-ing");

        // logic for down air here
    }
}
