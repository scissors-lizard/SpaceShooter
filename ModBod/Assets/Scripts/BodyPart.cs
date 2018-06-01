using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BodyPart : MonoBehaviour, IProjectileHandler
{
    public Body body;
    public float mass;
    public int maxHP;
    public int curHP;
    public bool[] validConnectors;
    public BuildCell gridCell;

    [SerializeField] protected GameObject deathFXPrefab;

    private void Awake()
    {
        curHP = maxHP;
    }

    public virtual void OnProjectileHit(Projectile p)
    {
        Destroy(p.gameObject);
    }


    public void SetBuildMode (bool isOn)
    {
        if (isOn)
        {
            enabled = false;
        }
        else
        {
            enabled = true;

        }
    }

    public virtual void Kill()
    {
        GameObject go = Instantiate(deathFXPrefab) as GameObject;
        go.transform.position = transform.position;

        body.RemovePart(this);
        Destroy(gameObject);
    }

}