using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BodyPart : MonoBehaviour, IProjectileHandler
{
    public Body body;
    public BodyPart parentPart;
    public float mass;
    public int maxHP;
    public int curHP;
    public bool[] validConnectors;
    public BuildCell gridCell;

    [SerializeField] protected GameObject deathFXPrefab;

    private List<BodyPart> children;

    private void Awake()
    {
        curHP = maxHP;
        children = new List<BodyPart>();
    }

    public virtual void OnProjectileHit(Projectile p)
    {
        Destroy(p.gameObject);
    }

    public void AddChildPart(BodyPart toAdd)
    {
        children.Add(toAdd);
        toAdd.body = body;
    }

    public void RemoveChildPart(BodyPart toRemove)
    {
        children.Remove(toRemove);
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

    protected virtual void OnKilled()
    {
        GameObject go = Instantiate(deathFXPrefab) as GameObject;
        go.transform.position = transform.position;
        for(int i = 0; i < children.Count; i++)
        {
            children[i].OnKilled();
        }
        body.RemovePart(this);
        parentPart.RemoveChildPart(this);
        Destroy(gameObject);
    }

}