using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : State
{
    public override void OnEnter(Animator p_anim)
    {
        Debug.Log("Enter Attack");
        p_anim.Play("DOWN_attack");
    }

    public override State RunCurrentState()
    {
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
}
