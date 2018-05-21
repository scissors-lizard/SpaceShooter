using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IProjectileHandler
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay;
    [SerializeField] private int maxHp;
    [SerializeField] private GameObject deathFXPrefab;

    private int hp;

    private float shotTimer;
    private Transform target;

    public enum State
    {
        Wander,
        Attack,
        Die
    }

    public State state;


    void Start()
    {
        shotTimer = shotDelay;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        hp = maxHp;
        NextState();
    }

    void NextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info =
            GetType().GetMethod(methodName,
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    IEnumerator WanderState()
    {
        while (state == State.Wander)
        {
            UpdateWander();
            yield return null;
        }
        NextState();
    }

    IEnumerator AttackState()
    {
        while (state == State.Attack)
        {
            UpdateAttack();
            yield return null;
        }
        NextState();
    }

    IEnumerator DieState()
    {
        while (state == State.Die)
        {
            UpdateDie();
            yield return null;
        }
    }

    private void UpdateWander()
    {
        // Check if player nearby and in view
    }
    private void UpdateAttack()
    {
        // Check if player nearby and in view
        RotateTowardsTarget();
        shotTimer -= Time.deltaTime;
        if(shotTimer <= 0f)
        {
            shotTimer += shotDelay;
            Fire();
        }
    }

    private void RotateTowardsTarget()
    {
        transform.up = target.position - transform.position;
    }

    private void Fire()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }

    private void UpdateDie()
    {
        // Check if player nearby and in view
    }

    public void OnProjectileHit(Projectile p)
    {
        Destroy(p.gameObject);
        hp -= 1;
        if (hp <= 0)
        {
            GameObject go = Instantiate(deathFXPrefab) as GameObject;
            go.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}