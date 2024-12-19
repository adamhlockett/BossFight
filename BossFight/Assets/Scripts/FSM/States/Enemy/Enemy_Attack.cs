using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : State
{
    public bool isComplete;

    //public Enemy_Idle idleState;
    //public bool toIdle;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        StartAnim(p_anim, p_enemy);
        StartCoroutine(WaitToIdle());
        isComplete = false;
        //toIdle = false;
    }

    public override void RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        ManageTransitions();
    }

    private void ManageTransitions()
    {
        //if (toIdle)
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
        isComplete = true;
    }

    public override bool CheckIfComplete() { return isComplete; }
}
