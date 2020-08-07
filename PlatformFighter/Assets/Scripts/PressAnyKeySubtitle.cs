using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyKeySubtitle : MonoBehaviour
{
    // public fields
    public Image image;
    public float imageBlinkTime = 1f;

    // private fields
    float blinkTimer = 0f;
    bool imageIsOn = true;

    void Start()
    {
        blinkTimer = imageBlinkTime;
    }

    // Update is called once per frame
    void Update()
    {
        blinkTimer -= Time.deltaTime;
        if (blinkTimer <= 0f)
        {
            toggle();
            blinkTimer = imageBlinkTime;
        }
    }

    /* toggle:
     *      toggles the image on or off
     *      Args:
     *      Returns:
     */
    void toggle()
    {
        if (imageIsOn)
        {
            image.enabled = false;
        } 
        else
        {
            image.enabled = true;
        }

        imageIsOn = !imageIsOn;
    }

}
