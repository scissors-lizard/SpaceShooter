using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart_Core : BodyPart {
    [SerializeField] private SpriteRenderer bodySprite;
    [SerializeField] private DamageFlash[] flashFX;

    public override void OnProjectileHit(Projectile p)
    {
        curHP -= 1;
        if (curHP <= 0)
        {
            Kill();
        }
        else
        {
            for(int i = 0; i < flashFX.Length; i++)
            {
                flashFX[i].Flash();
            }
        }
        Destroy(p.gameObject);
    }

    public override void Kill()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(deathFXPrefab) as GameObject;
            go.transform.position = transform.position + new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0f);
        }
        Destroy(body.gameObject);
    }

}
