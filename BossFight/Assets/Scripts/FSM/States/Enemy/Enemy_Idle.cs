using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : EnemyState
{
    public bool isComplete;

    [SerializeField] DynamicAdjuster d;

    //public float idleFor = 5f;
    //public float telegraphWarning = 0.5f;
    //public float detonateTelegraphWarning = 1f;
    [SerializeField] GameStates gameStates;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        stateName = "idle";
        isComplete = false;
        StartAnim(p_anim, p_enemy);
        if (d.dA.idleFor < 2) d.dA.idleFor = 2f;
        else if (d.dA.idleFor > 5) d.dA.idleFor = 5f;
        StartCoroutine(IdleFor(d.dA.idleFor));
        //Debug.Log(d.dA.idleFor);
    }

    public override void RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        ManageTransitions();
    }

    private void ManageTransitions()
    {

    }

    private void ManageLogic()
    {
        // does not have to do anything
        //if(isComplete) Debug.Log(isComplete);
    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_idle";
        p_anim.Play(animName);
    }

    IEnumerator IdleFor(float p_idleFor)
    {
        if (isComplete) yield break;
        yield return new WaitForSeconds(p_idleFor - d.dA.telegraphFor);
        if(!gameStates.inTraining) Instantiate(telegraphIndicator, GameObject.Find("TelegraphIndicator").transform.position, Quaternion.identity);
        yield return new WaitForSeconds(d.dA.telegraphFor);
        isComplete = true;
    }

    public override bool CheckIfComplete() { return isComplete; }
}
