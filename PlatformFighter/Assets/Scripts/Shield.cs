using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Vector3 scale;
    const float SHIELDDURATION = 3f;
    const float RECHARGETIME = 4f;
    float useToRechargeRatio = SHIELDDURATION / RECHARGETIME;
    float currentTime = SHIELDDURATION; 
    float ratio = 1f;   // determines the size of the shield
     
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    /* updateShield:
    *    should be called every update to update the size of the shield
    *    whether the player is hold shield or not
    */
    public bool updateShield(float time, bool isOn)
    {
        if (isOn)
        {
            if(currentTime > 0) // decrease timer
            {
                currentTime -= time;
            }

            if(currentTime <= 0)    // shield broken
            {
                transform.localScale = new Vector3(0, 0, 0);
                return true;
            }

            updateShieldSize();

        }
        else
        {
            if(currentTime < SHIELDDURATION)
            {
                currentTime += time * useToRechargeRatio;
            }
            updateShieldSize();
        }
        return false;
    }

    /* updateShieldSize:
    *    update size of shield base on the time ratio
    */

    void updateShieldSize()
    {
        ratio = currentTime / SHIELDDURATION;
        transform.localScale = new Vector3(scale.x * ratio, scale.y * ratio, scale.z * ratio);
        // updates shield size
    }
}
