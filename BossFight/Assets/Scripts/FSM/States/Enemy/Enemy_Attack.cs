using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : EnemyState
{
    public bool isComplete;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameStates gameStates;
    Enemy_Charge chargeState;

    [SerializeField] DynamicAdjuster d;

    public float fireAmount;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        stateName = "attack";
        StartAnim(p_anim, p_enemy);
        StartCoroutine(WaitToIdle());
        isComplete = false;
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

    IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(d.dA.fireFor);
        CancelInvoke("Fire");
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
