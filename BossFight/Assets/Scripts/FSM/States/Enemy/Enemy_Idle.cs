using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : State
{
    public Enemy_Chase chaseState;
    public bool canChase;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_fly";
        p_anim.Play(animName);
    }

    public override State RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        return ManageTransitions();
    }

    private State ManageTransitions()
    {
        if (canChase) return chaseState;

        else return this;
    }

    private void ManageLogic()
    {

    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_fly";
        p_anim.Play(animName);
    }
}
