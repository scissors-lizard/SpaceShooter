using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartCore : BodyPart {
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
