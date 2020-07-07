using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UsefulConstants;

public class Rectangle : PlayerController
{
    public GameObject shooterObject;

    ProjectileShooter shooter;

    void Start()
    {
        shooter = shooterObject.GetComponent<ProjectileShooter>();
        base.Start();
    }

    protected override void jab()
    {
        UnityEngine.Debug.Log("rectangle is jabbing");
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

    protected override void neutralB()
    {
        anime.setAnimator(AnimeState.NeutralB);
        shooter.shoot();
        lagTime = 0.2f;
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

    }

    protected override void downB()
    {
        UnityEngine.Debug.Log("rectangle is down b-ing");
        anime.setAnimator(AnimeState.DownB);
        if (!isGrounded)
        {
            fallMaxSpeed = -2f;
            rb.velocity = new Vector2(rb.velocity.x, -2);
        }
        hasControl = false;


    }

    protected override void releaseDownB()
    {
        anime.setAnimator(AnimeState.ReleaseB);
        fallMaxSpeed = -12f;
        
        hasControl = true;
        lagTime = 0.4f;
    }
}
