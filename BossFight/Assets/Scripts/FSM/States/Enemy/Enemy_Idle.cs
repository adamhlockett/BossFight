using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : State
{
    public Enemy_Charge chargeState;
    public bool canCharge;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        StartAnim(p_anim, p_enemy);
    }

    public override State RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        return ManageTransitions();
    }

    private State ManageTransitions()
    {
        if (canCharge) return chargeState; // changes state to chase

        else return this;
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
}
