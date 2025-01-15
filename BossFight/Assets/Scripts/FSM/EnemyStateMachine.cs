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
    [HideInInspector] public Enemy enemy;
    //[SerializeField] EnemyState[] states;
    [SerializeField] List<EnemyState> enemyStates;
    //public Dictionary<int, Dictionary<string, State>> stateDict;
    private int currentStateNum = 0;
    public bool canChangeState = false;
    [SerializeField] DynamicAdjuster d;

    private void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        enemy = transform.root.GetComponent<Enemy>();
        Restart();
    }

    public void Restart()
    {
        currentStateNum = 0;
        currentState = enemyStates[currentStateNum];
        if (anim != null ) { currentState.OnEnter(anim, enemy); }
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
        if (!canChangeState) { currentState = enemyStates[currentStateNum]; return; }
        d.CheckForAdjustments();
        currentStateNum++;
        if (currentStateNum >= enemyStates.Count) currentStateNum = 0;
        currentState = enemyStates[currentStateNum];
        currentState.OnEnter(anim, enemy);
    }
}
