using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState;
    private Animator anim;

    private void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        currentState.OnEnter(anim);
    }

    private void Update()
    {
       RunMachine();
    }

    private void RunMachine()
    {
        State nextState = currentState?.RunCurrentState(); // ? checks if NULL

        if (nextState != null && nextState != currentState)
        {
            SwitchState(nextState);
        }
    }

    private void SwitchState(State p_nextState)
    {
        currentState = p_nextState;
        currentState.OnEnter(anim);
    }
}
