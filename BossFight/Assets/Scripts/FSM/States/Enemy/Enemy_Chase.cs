using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Chase : State
{
    public Enemy_Attack attackState;
    public bool canAttack;
    public Transform playerPos;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        
    }

    public override State RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        return ManageTransitions();
    }

    private State ManageTransitions()
    {
        if (canAttack) return attackState;

        else return this;
    }

    private void ManageLogic()
    {
        // state logic here
        Debug.Log(this.gameObject.transform.position);
    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_fly";
        p_anim.Play(animName);
    }
}
