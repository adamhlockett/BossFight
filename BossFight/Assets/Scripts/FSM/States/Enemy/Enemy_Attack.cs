using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : State
{
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
        return this;
    }

    private void ManageLogic()
    {
        // state logic here
    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_attack";
        p_anim.Play(animName);
    }
}
