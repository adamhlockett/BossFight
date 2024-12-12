using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Chase : State
{
    public Enemy_Attack attackState;
    public bool canAttack;

    public override void OnEnter(Animator p_anim)
    {
        Debug.Log("Enter Chase");
        p_anim.Play("DOWN_special");
    }

    public override State RunCurrentState()
    {
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
    }


}
