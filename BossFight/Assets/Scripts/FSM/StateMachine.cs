using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Start State")]
    public State currentState;
    private Animator anim;
    public Enemy enemy;

    private void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        enemy = transform.root.GetComponent<Enemy>();
        currentState.OnEnter(anim, enemy);
    }

    private void Update()
    {
       RunMachine();
    }

    private void RunMachine()
    {
        State nextState = currentState?.RunCurrentState(anim, enemy); // ? checks if NULL

        if (nextState != null && nextState != currentState)
        {
            SwitchState(nextState);
        }
    }

    private void SwitchState(State p_nextState)
    {
        currentState = p_nextState;
        currentState.OnEnter(anim, enemy);
    }
}
