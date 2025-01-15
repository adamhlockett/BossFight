using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAdjuster : MonoBehaviour
{
    public DynamicAdjustments dA;
    private DefaultDynamicAdjustments _dA;
    private DynamicAdjustmentMethod method;
    [SerializeField] List<DynamicAdjustmentMethod> methodList;

    private float difficulty = 1;
    private float lastDifficulty;

    private void Awake()
    {
        dA = new DynamicAdjustments();
        _dA = new DefaultDynamicAdjustments();

        SetInitialValues();

        ApplyInitialValues();

        lastDifficulty = difficulty;

        method = methodList[PlayDataSingleton.instance.playMode];
    }

    private void SetInitialValues() //-------------------------------------------------------change defaults here
    {
        difficulty = 1;
        lastDifficulty = difficulty;
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
        difficulty = 1;
        lastDifficulty = difficulty;
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
        if (difficulty != lastDifficulty)
        {
            Adjustment();
            lastDifficulty = difficulty;
        }

        method.CheckForAdjustments();
    }

    public void Adjustment()
    {
        dA.detonateAfterTurns = _dA.defaultDetonateAfterTurns * (2 - difficulty); // - detonate sooner
        dA.telegraphFor = _dA.defaultTelegraphFor * (2 - difficulty); // - telegraph for less time
        dA.telegraphDetonateFor = _dA.defaultTelegraphDetonateFor * difficulty;             //SHOULD BE -, BUT proves too difficult
        dA.idleFor = _dA.defaultIdleFor * (2 - difficulty); // - idle for less time
        dA.slamRadius = _dA.defaultSlamRadius * difficulty;
        dA.slamDamage = _dA.defaultSlamDamage * difficulty;
        dA.chargeSpeed = _dA.defaultChargeSpeed * difficulty;
        dA.damageRate = _dA.defaultDamageRate * (2 - difficulty); // - damage more often
        dA.projectileDamage = _dA.defaultProjectileDamage * difficulty;
        dA.projectileSpeed = _dA.defaultProjectileSpeed * difficulty;
        dA.fireRate = _dA.defaultFireRate * (2 - difficulty); // - fire more often
        dA.fireFor = _dA.defaultFireFor * (2 - difficulty); // - fire for longer
    }
}                                          
