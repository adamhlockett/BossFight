using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : State
{
    public bool isComplete;

    [Header("Dynamic Adjustments")]
    public float idleFor = 5f;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        stateName = "idle";
        StartAnim(p_anim, p_enemy);
        StartCoroutine(WaitToCharge(idleFor));
        isComplete = false;
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
    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_fly";
        p_anim.Play(animName);
    }

    IEnumerator WaitToCharge(float p_idleFor)
    {
        yield return new WaitForSeconds(p_idleFor);
        isComplete = true;
    }

    public override bool CheckIfComplete() { return isComplete; }
}
