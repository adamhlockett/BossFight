using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : EnemyState
{
    public bool isComplete;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameStates gameStates;
    Enemy_Charge chargeState;

    DynamicAdjuster d;

    public float fireAmount;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        d = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
        stateName = "attack";
        StartAnim(p_anim, p_enemy);
        StartCoroutine(WaitToIdle());
        isComplete = false;
        //if(d.dA.fireRate < 1f) { d.dA.fireRate = 1f; } // apply highest difficulty bounds
        //if (d.dA.fireFor < 5) { d.dA.fireFor = 5f; }
        InvokeRepeating("Fire", 0.1f, d.dA.fireRate);
        fireAmount = d.dA.fireFor / d.dA.fireRate;
        chargeState = GameObject.Find("Charge").GetComponent<Enemy_Charge>();
    } 

    public override void RunCurrentState(Animator anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide) StartAnim(anim, p_enemy);

        ManageLogic();

        ManageTransitions();
    }

    private void ManageTransitions()
    {
        
    }

    private void ManageLogic()
    {

    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_attack";
        p_anim.Play(animName);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(d.dA.fireFor);
        StopFiring();
        isComplete = true;
    }

    public override bool CheckIfComplete() { return isComplete; }

    private void Fire()
    {
        if (gameStates.inTraining) return;
        Vector3 spawnPos = GameObject.Find("EndOfStaff").transform.position;
        Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
    }
}
