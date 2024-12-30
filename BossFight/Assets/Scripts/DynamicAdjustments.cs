using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct DynamicAdjustments
{
    [Header("Player")]
    float playerDamage;                           // from Player

    [Header("Enemy")]
    //+adjust states in array

    float detonateAfterTurns;                     //from Enemy

    float telegraphFor;                           //from Enemy_Idle
    float telegraphDetonateFor;                   //from Enemy_Idle

    float slamRadius;                             //from Enemy_Charge
    float slamDamage;                             //from Enemy_Charge
    float chargeSpeed;                            //from Enemy_Charge
    float damageRate;                             //from Enemy_Charge

    float projectileDamage;                       //from Enemy_Attack
    float projectileSpeed;                        //from Enemy_Attack
    float fireRate;                               //from Enemy_Attack
    float fireFor;                                //from Enemy_Attack
    float detonationSlamRadius;                   //from Enemy_Attack

}
