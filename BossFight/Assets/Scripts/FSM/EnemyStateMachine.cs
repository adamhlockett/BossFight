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
    public EnemyState currentState;
    private Animator anim;
    [HideInInspector] public Enemy enemy;
    //[SerializeField] EnemyState[] states;
    [SerializeField] List<EnemyState> enemyStates; // defined in editor
    //public Dictionary<int, Dictionary<string, State>> stateDict;
    private int currentStateNum = 0;
    public bool canChangeState = false;
    [SerializeField] DynamicAdjuster d;
    [SerializeField] Enemy_Attack attackState;
    [SerializeField] Enemy_Idle idleState;
    [SerializeField] Enemy_Charge chargeState;
    public string MCTSNextAttack;

    private void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        enemy = transform.root.GetComponent<Enemy>();
        Restart();
    }

    public void Restart()
    {
        attackState.isComplete = false;
        idleState.isComplete = false;
        chargeState.isComplete = false;
        currentStateNum = 0;
        if (anim != null ) { currentState.OnEnter(anim, enemy); }
        foreach (EnemyState enemy in enemyStates)
        {
            if (enemy.stateName == "attack") attackState.StopFiring(); 
        }
        currentState = enemyStates[currentStateNum];
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
            if(d.method.methodName == "Monte Carlo")
            {
                MCTSSwitchState();
            }
            else
            {
                SwitchState();
            }
        }
    }
    private void MCTSSwitchState()
    {
        if (!canChangeState) { currentState = enemyStates[0]; return; } // remain in same state
        d.CheckForAdjustments();

        attackState.isComplete = false;
        idleState.isComplete = false;
        chargeState.isComplete = false;

        if (MCTSNextAttack == "Charge")
        {
            currentState = enemyStates[1];
        }
        else if (MCTSNextAttack == "Attack")
        {
            currentState = enemyStates[3];
        }
        else
        {
            MCTSNextAttack = "Idle";
            currentState = enemyStates[0];
        }

        currentState.OnEnter(anim, enemy);
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
        //Debug.Log(currentState.stateName);
    }
}
