using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart_Gun : BodyPart {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float kickback;
    [SerializeField] private SpriteRenderer bodySprite;


    public override void OnProjectileHit(Projectile p)
    {
        Destroy(p.gameObject);
        curHP -= 1;
        if(curHP <= 0)
        {
            Destroy(gameObject);
        }
    }


	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(projectilePrefab) as GameObject;
            projectile.transform.position = firePoint.position;
            projectile.transform.up = firePoint.transform.up;

            rb.AddForceAtPosition(-transform.up * kickback, transform.position);
        }
            bodySprite.color = GetCurHealthColor();
    }
}
