using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public struct DynamicAdjustments
{
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
    public float defaultTelegraphDetonateFor;                   //from Projectile
    public float defaultIdleFor;                                //from Enemy_Idle

    public float defaultSlamRadius;                             //from Slam
    public float defaultSlamDamage;                             //from Slam
    public float defaultChargeSpeed;                            //from Slam
    public float defaultDamageRate;                             //from Enemy_Charge

    public float defaultProjectileDamage;                       //from Projectile
    public float defaultProjectileSpeed;                        //from Projectile
    public float defaultFireRate;                               //from Projectile
    public float defaultFireFor;                                //from Enemy_Attack
}
