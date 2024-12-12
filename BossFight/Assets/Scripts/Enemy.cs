using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

namespace UnityHFSM
{
    [RequireComponent(typeof(Animator))]
    public class Enemy : MonoBehaviour
    {
        //[SerializeField] private Transform player;
        //
        //private StateMachine<EnemyState, StateEvent> enemyFSM;
        //private Animator anim;
        //
        //private void Awake()
        //{
        //    anim = GetComponent<Animator>();
        //    enemyFSM = new StateMachine<EnemyState, StateEvent>();
        //
        //    enemyFSM.AddState(EnemyState.Idle, new EnemyIdle(false, this));
        //    enemyFSM.AddState(EnemyState.Fly, new EnemyFly(true, this, player));
        //    enemyFSM.AddState(EnemyState.Attack, new EnemyAttack(true, this, OnAttack));
        //    enemyFSM.AddState(EnemyState.Special, new EnemySpecial(true, this, OnSpecial));
        //    enemyFSM.AddState(EnemyState.Die, new EnemyDie(true, this, OnDie));
        //
        //    enemyFSM.SetStartState(EnemyState.Idle);
        //
        //    enemyFSM.Init();
        //}
        //
        //private void OnAttack(State<EnemyState,StateEvent> state)
        //{
        //
        //}
        //
        //private void OnSpecial(State<EnemyState, StateEvent> state)
        //{
        //
        //}
        //
        //private void OnDie(State<EnemyState, StateEvent> state)
        //{
        //
        //}
        //
        //private void Update()
        //{
        //    enemyFSM.OnLogic();
        //}

    }
}
