using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BodyPart : MonoBehaviour, IProjectileHandler
{
    public Body body;
    public BodyPart parentPart;
    public float mass;

    [SerializeField] protected GameObject deathFXPrefab;

    [SerializeField] private int maxHP;
    [SerializeField] private Gradient gradient;

    private Connector[] connectors;

    public int curHP;

    private List<BodyPart> children;

    private void Awake()
    {
        curHP = maxHP;
        children = new List<BodyPart>();
    }

    void Start()
    {
        connectors = GetComponentsInChildren<Connector>();
    }

    public Color GetCurHealthColor()
    {
        return gradient.Evaluate((float)curHP / maxHP);
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
            for (int i = 0; i < connectors.Length; i++)
            {
                connectors[i].SetBuildMode(true);
            }
        }
        else
        {
            enabled = true;
            for(int i = 0; i < connectors.Length; i++)
            {
                connectors[i].SetBuildMode(false);
            }
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