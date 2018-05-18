using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BodyPart : MonoBehaviour, IProjectileHandler
{

    public float mass;

    [SerializeField] private int maxHP;
    [SerializeField] private Gradient gradient;


    public int curHP;

    private void Awake()
    {
        curHP = maxHP;
    }

    public Color GetCurHealthColor()
    {
        return gradient.Evaluate((float)curHP / maxHP);
    }

    public virtual void OnProjectileHit(Projectile p)
    {
        Destroy(p.gameObject);
    }


}