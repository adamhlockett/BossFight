using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public struct DynamicAdjustments
{
    [Header("Player")]
    float playerDamage;                           // from Player

    [Header("Enemy")]

    public float detonateAfterTurns;                     //from Enemy

    public float telegraphFor;                           //from Enemy_Idle
    public float telegraphDetonateFor;                   //from Projectile
    public float idleFor;                                //from Enemy_Idle

    public float slamRadius;                             //from Slam
    public float slamDamage;                             //from Slam
    public float damageRate;                             //from Slam
    public float chargeSpeed;                            //from Enemy_Charge

    public float projectileDamage;                       //from Projectile
    public float projectileSpeed;                        //from Projectile
    public float fireRate;                               //from Enemy_Attack
    public float fireFor;                                //from Enemy_Attack
}

public struct DefaultDynamicAdjustments
{
    public float defaultDetonateAfterTurns;                     //from Enemy

    public float defaultTelegraphFor;                           //from Enemy_Idle
    public float defaultTelegraphDetonateFor;                   //from Enemy_Idle, used in Projectile
    public float defaultIdleFor;                                //from Enemy_Idle

    public float defaultSlamRadius;                             //from Enemy_Charge
    public float defaultSlamDamage;                             //from Enemy_Charge
    public float defaultChargeSpeed;                            //from Enemy_Charge
    public float defaultDamageRate;                             //from Enemy_Charge

    public float defaultProjectileDamage;                       //from Enemy_Attack
    public float defaultProjectileSpeed;                        //from Enemy_Attack
    public float defaultFireRate;                               //from Enemy_Attack
    public float defaultFireFor;                                //from Enemy_Attack
}
