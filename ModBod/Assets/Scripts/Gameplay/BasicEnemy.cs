using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    public enum State
    {
        Wander,
        Attack,
        Die
    }

    public State state;


    void Start()
    {
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
    }
    private void UpdateDie()
    {
        // Check if player nearby and in view
    }



}