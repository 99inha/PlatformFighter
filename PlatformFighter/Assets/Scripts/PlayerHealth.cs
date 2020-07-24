/* This class acts as the model for the player
 * This class will store common information that the player needs
 * The controller class will manipulate the values here
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mechanics;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] const float ZERO_KNOCKBACK_SCALE = 1f;
    [SerializeField] const float THIRTY_KNOCKBACK_SCALE = 1.25f;
    [SerializeField] const float SIXTY_KNOCKBACK_SCALE = 1.5f;
    [SerializeField] const float NINETY_KNOCKBACK_SCALE = 2f;
    [SerializeField] const float ONE_TWENTY_KNOCKBACK_SCALE = 3f;

    [SerializeField] int maxHealth;
    [SerializeField] int maxStocks;

    [SerializeField] float currHealth = 0f;
    [SerializeField] int currStocks;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI stockText;

    void Start()
    {
        currStocks = maxStocks;
        healthText.text = currHealth.ToString("N1");
        stockText.text = currStocks.ToString();
    }

    /* getHealth:
     *   getter for the current health of the player
     *      Args: none
     *      Returns: current health of the player
     */
    public float getHealth()
    {
        return currHealth;

    }

    /* takeDamage:
     *   decrements the health of the player based on the move used
     *      Args: Attack attack = carries the information regarding
     *            the attack received
     *      Returns: the knockback that the player will experience
     */
    public Vector2 takeDamage(Attack attack)
    {
        currHealth += attack.damage;
        Vector2 finalKnockback = attack.knockback;

        if (!attack.hasUniformKnockback)
        {
            if (currHealth < 30f)
            {
                finalKnockback *= ZERO_KNOCKBACK_SCALE;
            }
            else if (currHealth < 60f)
            {
                finalKnockback *= THIRTY_KNOCKBACK_SCALE;
            }
            else if (currHealth < 90f)
            {
                finalKnockback *= SIXTY_KNOCKBACK_SCALE;
            }
            else if (currHealth < 120f)
            {
                finalKnockback *= NINETY_KNOCKBACK_SCALE;
            }
            else
            {
                finalKnockback *= ONE_TWENTY_KNOCKBACK_SCALE;
            }
        }

        healthText.text = currHealth.ToString("N1");
        return finalKnockback;
    }

    /* die:
     *      decrements a stock from the player's life
     *      Args:
     *      Returns: int - the remaining lives
     */
    public int die()
    {
        currStocks--;
        stockText.text = currStocks.ToString();
        return currStocks;
    }

    /* respawn:
     *      resets the health of the player
     *      Args:
     *      Returns:
     */
    public void respawn()
    {
        currHealth = 0f;
        healthText.text = currHealth.ToString("N1");
    }
}
