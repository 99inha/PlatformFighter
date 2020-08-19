using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    float fadeOutTime = 1f;
    float currentTime;
    float audioVolume;
    float currentVolume;
    bool fade = false;
    [SerializeField] List<AudioClip> tracks;
    
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        currentTime = fadeOutTime;
        audioVolume = audio.volume;
        currentVolume = audioVolume;
        if(tracks.Count > 0)
        {
            audio.clip = tracks[Random.Range(0, tracks.Count-1)];
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            currentTime -= Time.deltaTime;
            audio.volume = audioVolume * (currentTime / fadeOutTime);
        }
    }

    public void startFade()
    {
        fade = true;
    }
}
