using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilisticMethod : DynamicAdjustmentMethod
{
    
    //retries count as loss, loss doesn't count at retry
    //probably need a reset method to be used in GameStates.cs

    PlayDataSingleton p = PlayDataSingleton.instance;
    float checkEvery, checkEverySeconds = 5f;


    private void Start()
    {
        methodName = "Probabilistic";

        p.shortestPlayTime = 5f;
        p.longestPlayTime = 15f;
        checkEvery = checkEverySeconds;
    }

    public override void CheckForAdjustments()
    {
        checkEvery -= Time.deltaTime;
        if (checkEvery > 0) { return; }
        else checkEvery = checkEverySeconds;

        

        CalculateTrajectories();

        CalculateLossChance();
        



        AdjustDifficulty();
    }

    private void CalculateTrajectories()
    {
        p.healthGap = (p.enemyHealth / p.enemyMaxHealth) - (p.playerHealth / p.playerMaxHealth);

        p.playerAccuracy = p.playerHits / p.playerAttacks;

        p.enemyAccuracy = p.enemyHits / p.enemyAttacks;

        p.avgPlayTime = p.totalPlayTime / p.attempts;
    }

    private void CalculateLossChance()
    {
        CalculatePlayTime();

        CalculateHealthGap();

        CalculateAccuracies();

        if (p.pr_loss < 0) p.pr_loss = 0;
        else if (p.pr_loss > 1) p.pr_loss = 1; // apply bounds
    }

    private void CalculatePlayTime()
    {
        if (p.playTime < p.shortestPlayTime) { p.pr_loss = 0; }

        else if (p.playTime >= p.shortestPlayTime && p.playTime <= p.longestPlayTime)
        {
            float playTimeRange = p.longestPlayTime - p.shortestPlayTime;
            float playTimeGap = p.playTime - p.shortestPlayTime;
            p.pr_loss = playTimeGap / playTimeRange;
        }

        else if (p.playTime > p.longestPlayTime) { p.pr_loss = 1; }
    }

    private void CalculateHealthGap()
    {
        if (((p.pr_loss += p.healthGap) > 0) && ((p.pr_loss += p.healthGap) < 1))
        {
            p.pr_loss += p.healthGap;
        }
    }

    private void CalculateAccuracies()
    {
        if (p.playerHealth <= p.enemyDamage)
        {
            p.pr_loss += p.enemyAccuracy;
        }

        if (p.enemyHealth <= p.playerDamage)
        {
            p.pr_loss -= p.playerAccuracy;
        }
    }

    public override void AdjustDifficulty()
    {
        float difficulty = 2 - (p.pr_loss * 2); // difficulty is from 0-2 easy to hard, loss chance is 0-1 win to loss
        if (difficulty < 0.6) difficulty = 0.6f;
        else if (difficulty > 1.6) difficulty = 1.6f; // apply bounds
        p.difficulty = difficulty;

        Debug.Log(p.difficulty + " difficulty");
    }
}

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
//}
//try { PlayDataSingleton.instance.healthGap = (int)(((PlayDataSingleton.instance.enemyHealth / PlayDataSingleton.instance.enemyMaxHealth) * 100) -
//        ((PlayDataSingleton.instance.playerHealth / PlayDataSingleton.instance.playerMaxHealth) * 100)); }
//catch (DivideByZeroException ex) { }

//accuracies
//if(PlayDataSingleton.instance.playerAttacks != 0)
//{
//}

//if(PlayDataSingleton.instance.enemyAttacks != 0) 
//{
//}

//need playtime to establish average

//chance of player losing level
// compare current playtime to average playtime

//if player is one hit, apply enemy accuracy to loss chance positively

//if enemy is one hit, apply player accuracy to loss chance negatively