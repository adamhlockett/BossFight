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
    //EnemyState[] enemyStates;
    [SerializeField] List<EnemyState> enemyStates;
    //public Dictionary<int, Dictionary<string, State>> stateDict;
    private int currentStateNum = 0;
    public bool canChangeState = false;
    DynamicAdjuster d;
    [SerializeField] Enemy_Attack attackState;
    [SerializeField] Enemy_Idle idleState;
    [SerializeField] Enemy_Charge chargeState;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        d = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
        //idleState = GameObject.FindGameObjectWithTag("Idle State").GetComponent<Enemy_Idle>();
        //idleState = transform.Find("States").Find("Idle").GetComponent<Enemy_Idle>();
        idleState = transform.Find("States").GetComponentInChildren<Enemy_Idle>();
        attackState = transform.Find("States").GetComponentInChildren<Enemy_Attack>();
        chargeState = transform.Find("States").GetComponentInChildren<Enemy_Charge>();
        //chargeState = GameObject.FindGameObjectWithTag("Charge State").GetComponent<Enemy_Charge>();
        //chargeState = transform.Find("States").Find("Charge").GetComponent<Enemy_Charge>();
        //attackState = GameObject.FindGameObjectWithTag("Attack State").GetComponent<Enemy_Attack>();
        //attackState = transform.Find("States").Find("Attack").GetComponent<Enemy_Attack>();
        //enemyStates[0] = idleState;
        //enemyStates[1] = chargeState;
        //enemyStates[2] = idleState;
        //enemyStates[3] = attackState;
        //enemyStates.Add(idleState);
        //enemyStates.Add(chargeState);
        //enemyStates.Add(idleState);
        //enemyStates.Add(attackState);

        anim = transform.root.GetComponent<Animator>();
        enemy = transform.root.GetComponent<Enemy>();
        attackState.isComplete = false;
        idleState.isComplete = false;
        chargeState.isComplete = false;
        currentStateNum = 0;
        currentState = enemyStates[currentStateNum];
        if (anim != null ) { currentState.OnEnter(anim, enemy); }
        foreach (EnemyState enemy in enemyStates)
        {
            if (enemy.stateName == "attack") attackState.StopFiring(); 
        }
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
        if (!canChangeState) { currentState = enemyStates[currentStateNum]; return; } // remain in same state
        d.CheckForAdjustments();
        currentStateNum++;
        if (currentStateNum >= enemyStates.Count) currentStateNum = 0;

        attackState.isComplete = false;
        idleState.isComplete = false;
        chargeState.isComplete = false;

        currentState = enemyStates[currentStateNum]; // change state
        currentState.OnEnter(anim, enemy);
        Debug.Log(currentState.stateName);
    }
}
