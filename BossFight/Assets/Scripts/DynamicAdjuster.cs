using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAdjuster : MonoBehaviour
{
    public DynamicAdjustments dA;
    private DefaultDynamicAdjustments _dA;

    private float difficulty = 1;
    private float lastDifficulty;

    private void Awake()
    {
        dA = new DynamicAdjustments();
        _dA = new DefaultDynamicAdjustments();

        SetInitialValues();

        ApplyInitialValues();

        lastDifficulty = difficulty;
    }

    private void SetInitialValues()
    {
        _dA.defaultDetonateAfterTurns = 3;
        _dA.defaultTelegraphFor = 1;
        _dA.defaultTelegraphDetonateFor = 1.5f;
        _dA.defaultIdleFor = 5;
        _dA.defaultSlamRadius = 0.75f;
        _dA.defaultSlamDamage = 15;
        _dA.defaultDamageRate = 2;
        _dA.defaultChargeSpeed = 5;
        _dA.defaultProjectileDamage = 10;
        _dA.defaultProjectileSpeed = 7;
        _dA.defaultFireRate = 1;
        _dA.defaultFireFor = 5;
    }

    public void ApplyInitialValues()
    {
        dA.detonateAfterTurns = _dA.defaultDetonateAfterTurns;
        dA.telegraphFor = _dA.defaultTelegraphFor;
        dA.telegraphDetonateFor = _dA.defaultTelegraphDetonateFor;
        dA.idleFor = _dA.defaultIdleFor;
        dA.slamRadius = _dA.defaultSlamRadius;
        dA.slamDamage = _dA.defaultSlamDamage;           
        dA.chargeSpeed = _dA.defaultChargeSpeed;          
        dA.damageRate = _dA.defaultDamageRate;           
        dA.projectileDamage = _dA.defaultProjectileDamage;
        dA.projectileSpeed = _dA.defaultProjectileSpeed;      
        dA.fireRate = _dA.defaultFireRate;             
        dA.fireFor = _dA.defaultFireFor;              
    }

    private void Update()
    {
        if (Input.GetButtonDown("Debug"))
        {
            difficulty += 0.1f;
            Debug.Log("difficulty" + difficulty);
            if(difficulty > 2) difficulty = 0;
        }

        if (difficulty != lastDifficulty)
        {
            Adjust();
            lastDifficulty = difficulty;
        }
    }

    public void Adjust()
    {
        dA.detonateAfterTurns = _dA.defaultDetonateAfterTurns * difficulty;
        dA.telegraphFor = _dA.defaultTelegraphFor * difficulty;
        dA.telegraphDetonateFor = _dA.defaultTelegraphDetonateFor * difficulty;
        dA.idleFor = _dA.defaultIdleFor * difficulty;
        dA.slamRadius = _dA.defaultSlamRadius * difficulty;
        dA.slamDamage = _dA.defaultSlamDamage * difficulty;
        dA.chargeSpeed = _dA.defaultChargeSpeed * difficulty;
        dA.damageRate = _dA.defaultDamageRate * difficulty;
        dA.projectileDamage = _dA.defaultProjectileDamage * difficulty;
        dA.projectileSpeed = _dA.defaultProjectileSpeed * difficulty;
        dA.fireRate = _dA.defaultFireRate * difficulty;
        dA.fireFor = _dA.defaultFireFor * difficulty;
    }
}                                          
