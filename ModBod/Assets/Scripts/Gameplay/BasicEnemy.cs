using DG.Tweening;
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
    [SerializeField] private DamageFlash damageFX;
    [SerializeField] private int shotsPerAttackCycle;

    private int shotsFired;
    private int hp;

    private float shotTimer;
    private Transform target;
  
    public enum State
    {
        Wander,
        Attack,
        Die,
        Enter
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

    IEnumerator EnterState()
    {
        Vector3 targetScale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(targetScale, 0.75f);
        yield return new WaitForSeconds(0.75f);

        state = State.Wander;
        NextState();
    }

    IEnumerator WanderState()
    {
        Vector3 wanderTargetPos = transform.position + new Vector3(UnityEngine.Random.value * 2f - 1f, UnityEngine.Random.value * 2f - 1f, 0f);
        transform.DOMove(wanderTargetPos, 2f).SetEase(Ease.InOutQuad);
        float timer = 2f;
        while (state == State.Wander)
        {
            yield return null;
            timer -= Time.deltaTime;
            if(timer <= 0f)
            {
                state = State.Attack;
            }
        }
        NextState();
    }

    IEnumerator AttackState()
    {
        shotsFired = 0;
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

    private void UpdateAttack()
    {
        //transform.Rotate(new Vector3(0f, 0f, 500f * Time.deltaTime));

        shotTimer -= Time.deltaTime;
        if(shotTimer <= 0f)
        {
            shotTimer += shotDelay;
            Fire();
        }
        if(shotsFired >= shotsPerAttackCycle)
        {
            shotsFired = 0;
            state = State.Wander;
        }
    }

    private void RotateTowardsTarget()
    {
        transform.up = target.position - transform.position;
    }

    private void Fire()
    {
        shotsFired++;
        GameObject bullet = GameObject.Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = transform.position;
        bullet.transform.up = target.position - bullet.transform.position;
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
        else
        {
            damageFX.Flash();
        }
    }
}