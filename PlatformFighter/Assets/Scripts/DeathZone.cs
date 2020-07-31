using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;
using System;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        UnityEngine.Debug.Log("someone died: " + col.name + ":" + gameObject.name.Substring(9));
        col.GetComponentInParent<PlayerController>().playerDeath(Convert.ToInt32(gameObject.name.Substring(9)));
    }
}
