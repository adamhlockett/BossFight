using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAdjuster : MonoBehaviour
{
    public DynamicAdjustments dA;
    private DefaultDynamicAdjustments _dA;
    private DynamicAdjustmentMethod method;
    [SerializeField] List<DynamicAdjustmentMethod> methodList;
    PlayDataSingleton p = PlayDataSingleton.instance;

    private float lastDifficulty;

    private void Awake()
    {
        dA = new DynamicAdjustments();
        _dA = new DefaultDynamicAdjustments();

        SetInitialValues();

        ApplyInitialValues();

        p.difficulty = 1;
        lastDifficulty = p.difficulty;

        method = methodList[PlayDataSingleton.instance.playMode];
    }

    private void SetInitialValues() //-------------------------------------------------------change defaults here
    {
        p.difficulty = 1;
        lastDifficulty = p.difficulty;
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
        p.difficulty = 1;
        lastDifficulty = p.difficulty;
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
        if (p.difficulty != lastDifficulty)
        {
            Adjustment();
            lastDifficulty = p.difficulty;
        }
        Debug.Log(p.difficulty);
    }

    public void CheckForAdjustments()
    {
        method.CheckForAdjustments();
    }

    public void Adjustment()
    {
        dA.detonateAfterTurns = _dA.defaultDetonateAfterTurns * (2 - p.difficulty); // - detonate sooner
        dA.telegraphFor = _dA.defaultTelegraphFor * (2 - p.difficulty); // - telegraph for less time
        dA.telegraphDetonateFor = _dA.defaultTelegraphDetonateFor * p.difficulty;             //SHOULD BE -, BUT proves too difficult
        dA.idleFor = _dA.defaultIdleFor * (2 - p.difficulty); // - idle for less time
        dA.slamRadius = _dA.defaultSlamRadius * p.difficulty;
        dA.slamDamage = _dA.defaultSlamDamage * p.difficulty;
        dA.chargeSpeed = _dA.defaultChargeSpeed * p.difficulty;
        dA.damageRate = _dA.defaultDamageRate * (2 - p.difficulty); // - damage more often
        dA.projectileDamage = _dA.defaultProjectileDamage * p.difficulty;
        dA.projectileSpeed = _dA.defaultProjectileSpeed * p.difficulty;
        dA.fireRate = _dA.defaultFireRate * (2 - p.difficulty); // - fire more often
        dA.fireFor = _dA.defaultFireFor * (2 - p.difficulty); // - fire for longer
    }
}                                          
