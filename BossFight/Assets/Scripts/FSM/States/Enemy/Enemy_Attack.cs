using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : State
{
    public bool isComplete;
    [SerializeField] GameObject projectilePrefab;
    Enemy_Charge chargeState;

    [Header("Dynamic Adjustments")]
    public float damage = 10f; 
    public float speed = 7f;
    public float canFireEvery = 1f;
    public float attackFor = 5f;
    public float fireAmount;
    public float projectileSlamSize = 0.5f;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        stateName = "attack";
        StartAnim(p_anim, p_enemy);
        StartCoroutine(WaitToIdle());
        isComplete = false;
        InvokeRepeating("Fire", 0.1f, canFireEvery);
        fireAmount = attackFor / canFireEvery;
        chargeState = GameObject.Find("Charge").GetComponent<Enemy_Charge>();
        chargeState.slamPrefabSize = projectileSlamSize;
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
        // state logic here

    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + "_attack";
        p_anim.Play(animName);
    }

    IEnumerator WaitToIdle()
    {
        yield return new WaitForSeconds(attackFor);
        CancelInvoke("Fire");
        isComplete = true;
    }

    public override bool CheckIfComplete() { return isComplete; }

    private void Fire()
    {
        Vector3 spawnPos = GameObject.Find("EndOfStaff").transform.position;
        Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
    }
}
