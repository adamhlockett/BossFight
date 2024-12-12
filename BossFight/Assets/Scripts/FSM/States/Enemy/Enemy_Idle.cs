using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Idle : State
{
    public Enemy_Chase chaseState;
    public bool canChase;

    public override void OnEnter(Animator p_anim)
    {
        Debug.Log("Enter Idle");
        p_anim.Play("DOWN_fly");
    }

    public override State RunCurrentState()
    {
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
        // state logic here
    }
}
