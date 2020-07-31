using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringout : MonoBehaviour
{
    // Start is called before the first frame update

    Animator ringAnime;

    float upperLimit = 12.73f;
    float lowerLimit = -11.45f;
    float leftLimit = -18.9f;
    float rightLimit = 22.2f;



    void getAnimator()
    {
        ringAnime = GetComponent<Animator>();
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
    

    public void playRingout(float x, float y)
    {
        Debug.Log(x + ":" + y);
        if(ringAnime != null)
        {
            ringAnime.SetTrigger("playRingout");
        }
        else
        {
            getAnimator();  // for some reason the unity start function was not running at the start
            ringAnime.SetTrigger("playRingout");
        }
    }

    
}
