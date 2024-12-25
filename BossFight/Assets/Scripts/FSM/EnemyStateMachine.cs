using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum StateNames
//{
//    Idle = 0,
//    Charge = 1,
//    Attack = 2
//}

public class EnemyStateMachine : MonoBehaviour
{
    [Header("Start State")]
    private EnemyState currentState;
    private Animator anim;
    public Enemy enemy;
    public EnemyState[] states;
    //public Dictionary<int, Dictionary<string, State>> stateDict;
    private int currentStateNum = 0;

    private void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        enemy = transform.root.GetComponent<Enemy>();
        currentState = states[0];
        currentState.OnEnter(anim, enemy);
    }

    private void Update()
    {
        RunMachine();
    }

    private void RunMachine()
    {
        ////Debug.Log((StateNames)currentStateNum);
        //Debug.Log(currentState.stateName);

        currentState?.RunCurrentState(anim, enemy);
        if ( currentState.CheckIfComplete() )
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        currentStateNum++;
        if (currentStateNum >= states.Length) currentStateNum = 0;
        currentState = states[currentStateNum];
        currentState.OnEnter(anim, enemy);
    }
}
