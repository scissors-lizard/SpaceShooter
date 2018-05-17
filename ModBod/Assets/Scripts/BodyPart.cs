using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyPart : MonoBehaviour {
    public abstract void OnProjectileHit(Projectile p);
    public float mass;
}
