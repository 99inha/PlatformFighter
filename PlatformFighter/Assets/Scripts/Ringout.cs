using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringout : MonoBehaviour
{
    // Start is called before the first frame update

    Animator ringAnime;


    // bounds for ring out 
    float upperLimit = 12.73f;
    float lowerLimit = -11.45f;
    float leftLimit = -18.9f;
    float rightLimit = 22.2f;


    private void destroy()
    {
        Destroy(gameObject);
    }
    

    public void playRingout(int deathZone)
    {
        //Debug.Log(x + ":" + y);
        Vector2 temp = gameObject.transform.position;
        switch (deathZone)
        {
            case 0:
                // bottom deathZone
                temp.y = lowerLimit + 3f;
                
                break;
            case 1:
                // left deathZone
                gameObject.transform.Rotate(0, 0, 90);
                temp.x = leftLimit + 3f;
                break;
            case 2:
                // top deathZone
                gameObject.transform.Rotate(0, 0, 180);
                temp.y = upperLimit - 3f;
                break;
            case 3:
                // right deathZone
                gameObject.transform.Rotate(0, 0, -270);
                temp.x = rightLimit - 3f;
                break;
        }

        float boundsFix = 1.5f;

        if(temp.x < leftLimit)
        {
            temp.x = leftLimit + boundsFix;
        }
        else if(temp.x > rightLimit)
        {
            temp.x = rightLimit - boundsFix;
        }

        if (temp.y < lowerLimit)
        {
            temp.y = lowerLimit + boundsFix;
        }
        else if (temp.y > upperLimit)
        {
            temp.y = upperLimit - boundsFix;
        }


        gameObject.transform.position = temp;

    }

    
}
