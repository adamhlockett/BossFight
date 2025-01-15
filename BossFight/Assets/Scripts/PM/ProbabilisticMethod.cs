using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilisticMethod : DynamicAdjustmentMethod
{
    
    //retries count as loss, loss doesn't count at retry
    //probably need a reset method to be used in GameStates.cs

    PlayDataSingleton p = PlayDataSingleton.instance;


    private void Start()
    {
        methodName = "Probabilistic";
    }

    public override void CheckForAdjustments()
    {
        //progression trajectories are:
        //how many times the player has lost - 1,
        //the health of the player - 2,
        //the health of the enemy - 2,
        //----- calculate health gap (percentage)
        //how many hits the player has hit/missed - 3,
        //how many hits the enemy has hit/missed - 4,
        //----- calculate hit accuracy
        //how many times the player has retried - 5
        //average playtime - 1, 5

        //establish probability of:
        //the player losing - 1 2,
        //the player hitting their attack - 3,
        //the enemy hitting their attack - 4,
        //the player retrying the level - 5

        /*-----------------------------------------------------------------------------------------------------------*/

        //handle losses and retries

        //negative health gap means player has higher percentage than the enemy, means they are winning
        //if(PlayDataSingleton.instance.enemyMaxHealth != 0 && PlayDataSingleton.instance.playerMaxHealth != 0) // this is likely unnecessary now
        //{
            p.healthGap = (int)(((p.enemyHealth / p.enemyMaxHealth) * 100) -
                ((p.playerHealth / p.playerMaxHealth) * 100));
        //}
        //try { PlayDataSingleton.instance.healthGap = (int)(((PlayDataSingleton.instance.enemyHealth / PlayDataSingleton.instance.enemyMaxHealth) * 100) -
        //        ((PlayDataSingleton.instance.playerHealth / PlayDataSingleton.instance.playerMaxHealth) * 100)); }
        //catch (DivideByZeroException ex) { }

        //accuracies
        //if(PlayDataSingleton.instance.playerAttacks != 0)
        //{
            p.playerAccuracy = (int)((p.playerHits / p.playerAttacks) * 100);
        //}

        //if(PlayDataSingleton.instance.enemyAttacks != 0) 
        //{
            p.enemyAccuracy = (int)((p.enemyHits / p.enemyAttacks) * 100);
        //}

        //need playtime to establish average
        p.avgPlayTime = p.totalPlayTime / p.losses;



        //chance of player losing



        //chance of player hitting next attack



        //chance of enemy hitting next attack



        //chance of player retrying level
        // compare current playtime to average playtime


    }

    public override void Adjust()
    {

    }
}
