using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : State
{
    public Enemy_Idle idleState;
    public bool toIdle;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        StartAnim(p_anim, p_enemy);
        StartCoroutine(WaitToIdle());
    }

    public override State RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        return ManageTransitions();
    }

    private State ManageTransitions()
    {
        if (toIdle) return idleState;

        else return this;
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

    IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(2);
        toIdle = true;
    }
}
