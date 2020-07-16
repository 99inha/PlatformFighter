using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectBullet : MonoBehaviour
{
    public float speed = 16f;
    public float lifeTime = 1f;
    public Rigidbody2D rb;
    public Vector2 knockback;
    public float damage;

    const int HURTBOXLAYER = 10;

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
        if (col.gameObject.layer == HURTBOXLAYER && col.tag != transform.tag)
        {
            UnityEngine.Debug.Log(col.name);

            // calculating the direction of the knockback
            float directionX = col.transform.position.x - transform.position.x;
            knockback.x *= directionX / Mathf.Abs(directionX);

            col.GetComponentInParent<MovementTest>().takeDamage(damage, knockback);

            Destroy(gameObject);
        }
        
    }
}
