using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Charge : State
{
    public Enemy_Attack attackState;
    public bool canAttack;
    private Vector2 chargeTo;
    private float distance;
    public float speed;
    private string animString;
    private bool hasReachedPoint;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        chargeTo = p_enemy.GetChargeToPoint();
        animString = "_fly";
        StartAnim(p_anim, p_enemy);
        hasReachedPoint = false;
    }

    public override State RunCurrentState(Animator p_anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide && animString != "_special") StartAnim(p_anim, p_enemy);

        ManageLogic(p_anim, p_enemy);

        return ManageTransitions();
    }

    private State ManageTransitions()
    {
        if (canAttack) return attackState; // changes state to attack

        else return this;
    }

    private void ManageLogic(Animator p_anim, Enemy p_enemy)
    {
        //charge toward
        transform.root.position = Vector2.MoveTowards(transform.root.position, chargeTo, speed * Time.deltaTime);

        //check if reached destination
        if (Vector3.Distance(transform.root.position, chargeTo) <= 0.2f && !hasReachedPoint)
        {
            animString = "_special";
            StartAnim(p_anim, p_enemy);
            StartCoroutine(WaitForSlam());
            hasReachedPoint = true;
        }
    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + animString;
        p_anim.Play(animName);
    }

    IEnumerator WaitForSlam()
    {
        yield return new WaitForSeconds(1);
        canAttack = true;
    }
}
