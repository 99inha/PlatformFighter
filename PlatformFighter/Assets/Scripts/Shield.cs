using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Vector3 scale;
    const float SHIELDMAXHEALTH = 150f;
    const float SHIELDDURATION = 2f;
    const float RECHARGETIME = 4f;
    float useToRechargeRatio = SHIELDDURATION / RECHARGETIME;
    float currentTime = SHIELDDURATION;     // the time represent the health of the shield 
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

    // resetShield: reset the health of the shield
    public void resetShield()
    {
        currentTime = SHIELDDURATION;
    }

    // takeDamage: decrease the health of the shield based on the inputed damage
    public void takeDamage(Attack attack)
    {
        float damageToTime = (attack.damage / SHIELDMAXHEALTH) * SHIELDDURATION;    // convert damage to time
        updateShield(damageToTime, true);
    }

    /* updateShieldSize:
    *    update size of shield base on the time ratio
    */


    // updateShieldSize: updates the size of the shield based on how much health
    //      the shield have
    void updateShieldSize()
    {
        ratio = currentTime / SHIELDDURATION;
        transform.localScale = new Vector3(scale.x * ratio, scale.y * ratio, scale.z * ratio);
        // updates shield size
    }

    


}
