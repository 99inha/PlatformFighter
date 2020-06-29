/* This class acts as the model for the player
 * This class will store common information that the player needs
 * The controller class will manipulate the values here
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // public fields; mutable through unity editor
    public int maxHealth;

    // private fields
    private int currHealth;

    /* getHealth:
     *   getter for the current health of the player
     *      Args: none
     *      Returns: current health of the player
     */
    public int getHealth()
    {
        return currHealth;
    }

    /* takeDamage:
     *   decrements the health of the player based on the move used
     *      Args: a kind of data structure to carry the kind of move or
     *            the amount of damage taken, to decrement the health by
     *            (unimplemented yet! need to implement)
     *      Returns: nothing
     */
    public void takeDamage()
    {

    }
}
