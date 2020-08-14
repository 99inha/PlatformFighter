using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip startGame;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //
    public void playSound(string name)
    {
        if(string.Compare(name, "start") == 0)
        {
            audio.PlayOneShot(startGame);
        }
    }
}
