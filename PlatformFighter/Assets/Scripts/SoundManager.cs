using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip startGame, hitEffect, ringoutEffect;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // playSound (int): play a clip based on with integer was given
    //                  0 -> start game sound effect
    //                  1 -> hit sound effect
    //                  2 -> ringout sound effect
    public void playSound(int effect)
    {
        if(effect == 0 && startGame != null)
        {
            audio.PlayOneShot(startGame);
        }
        else if(effect == 1 && hitEffect != null)
        {
            audio.PlayOneShot(hitEffect);
        }
        else if (effect == 2 && ringoutEffect != null)
        {
            audio.PlayOneShot(ringoutEffect);
        }
    }
}
