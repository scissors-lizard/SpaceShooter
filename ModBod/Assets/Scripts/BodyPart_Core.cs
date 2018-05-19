using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart_Core : BodyPart {
    [SerializeField] private SpriteRenderer bodySprite;

    public override void OnProjectileHit(Projectile p)
    {
        curHP -= 1;
        if (curHP <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        bodySprite.color = GetCurHealthColor();
    }

}
