using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Charge : EnemyState
{
    public bool isComplete;
    private Vector2 chargeTo;
    private string animString;
    private bool hasReachedPoint;
    [SerializeField] GameObject slamPrefab;

    [Header("Dynamic Adjustments")]
    public float defaultSlamPrefabSize = 1f;
    public float slamPrefabSize;
    public float damage = 15f;
    public float speed = 5f;
    public float canDamageEvery = 2f;

    public override void OnEnter(Animator p_anim, Enemy p_enemy)
    {
        stateName = "charge";
        slamPrefabSize = defaultSlamPrefabSize;
        chargeTo = p_enemy.GetPlayerPos();
        animString = "_fly";
        StartAnim(p_anim, p_enemy);
        hasReachedPoint = false;
        isComplete = false;
    }

    public override void RunCurrentState(Animator p_anim, Enemy p_enemy)
    {
        if (p_enemy.playerHasChangedSide && animString != "_special") StartAnim(p_anim, p_enemy);

        ManageLogic(p_anim, p_enemy);

        ManageTransitions();
    }

    private void ManageTransitions()
    {
        
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
            StartCoroutine(WaitForSlamPrefab());
        }
    }

    public override void StartAnim(Animator p_anim, Enemy p_enemy)
    {
        string animName = p_enemy.GetEnemyShouldFace() + animString;
        p_anim.Play(animName);
    }

    IEnumerator WaitForSlam()
    {
        //telegraphIndicator.SetActive(true);
        yield return new WaitForSeconds(1);
        isComplete = true;
    }

    IEnumerator WaitForSlamPrefab()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 spawnPos = new Vector3(transform.root.position.x, transform.root.position.y - 0.75f, transform.root.position.z);
        Instantiate(slamPrefab, spawnPos, Quaternion.identity);
        //telegraphIndicator.SetActive(false);
    }

    public override bool CheckIfComplete() { return isComplete; }
}
