using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [Header("Start State")]
    private State currentState;
    private Animator anim;
    public Enemy enemy;
    public State[] states;
    private int currentStateNum = 0;

    private void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        enemy = transform.root.GetComponent<Enemy>();
        currentState = states[currentStateNum];
        currentState.OnEnter(anim, enemy);
    }

    private void Update()
    {
        RunMachine();
    }

    private void RunMachine()
    {
        //State nextState = //currentState?.RunCurrentState(anim, enemy); // ? checks if NULL

        //if (nextState != null && nextState != currentState)
        currentState?.RunCurrentState(anim, enemy);
        if ( currentState.CheckIfComplete() )
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        currentStateNum += 1;
        if (currentStateNum >= states.Length) currentStateNum = 0;
        currentState = states[currentStateNum];
        currentState.OnEnter(anim, enemy);
    }
}
