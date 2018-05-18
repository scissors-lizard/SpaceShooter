using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed, maxLifetime;

    [SerializeField] private Rigidbody2D rb;

    private float timer;



    private void Start()
    {
        timer = maxLifetime;
        rb.velocity = transform.up * speed;
    }

    private void Update()
    {
        //transform.position += transform.up * speed * Time.deltaTime;
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }

    }

    private void LateUpdate()
    {
        transform.up = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject);
        IProjectileHandler handler = collision.collider.GetComponent<IProjectileHandler>();

        //IProjectileHandler handler = collision.gameObject.GetComponent<IProjectileHandler>();
        if(handler != null)
        {
            handler.OnProjectileHit(this);
        }

    }
}
