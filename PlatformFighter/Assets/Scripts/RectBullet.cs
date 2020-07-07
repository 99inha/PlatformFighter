using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectBullet : MonoBehaviour
{
    public float speed = 16f;
    public float lifeTime = 1f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        //logic for hitting here
    }
}
