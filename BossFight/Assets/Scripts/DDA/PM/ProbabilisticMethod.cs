using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilisticMethod : DynamicAdjustmentMethod
{
    PlayDataSingleton p = PlayDataSingleton.instance; //Contains all variables to be adjusted.

    float maxDifficulty = 1.8f, minDifficulty = 0.6f, playTimeWeighting = 0.5f;


    private void Start()
    {
        methodName = "Probabilistic";

        p.shortestPlayTime = 15f; 
        p.longestPlayTime = 25f;
        //Default values used before enough playtime data is gathered.
    }


    public override void CheckForAdjustments() //Called by EnemyFSM when switching states.
    {
        //Only adjust difficulty if player or enemy has taken damage.
        if (p.enemyHealth == p.enemyMaxHealth && 
            p.playerHealth == p.playerMaxHealth) p.difficulty = 1;

        else CalculateTrajectories();
    }


    private void CalculateTrajectories() //Calculate trajectories needed to establish loss chance.
    {
        p.healthGap = (p.enemyHealth / p.enemyMaxHealth) - (p.playerHealth / p.playerMaxHealth);
        //Gap in health between player and enemy.

        p.playerAccuracy = p.playerHits / p.playerAttacks;

        p.enemyAccuracy = p.enemyHits / p.enemyAttacks;
        //How many hits player and enemy have landed out of how many attacks they've done.

        p.avgPlayTime = p.totalPlayTime / p.attempts;
        //Average elapsed play time per attempt.

        CalculatePlayTime();
    }


    private void CalculatePlayTime()
    {
        if (p.playTime < p.shortestPlayTime) p.pr_loss = 0;
        /*p.pr_loss is our loss chance. If the player has not yet surpassed their shortest
         elapsed playtime, it is not regarded as possible for them to lose.*/

        else if (p.playTime >= p.shortestPlayTime && p.playTime <= p.longestPlayTime)
        {
            float playTimeRange = p.longestPlayTime - p.shortestPlayTime;
            float playTimeGap = p.playTime - p.shortestPlayTime;
            p.pr_loss = (playTimeGap / playTimeRange) * playTimeWeighting;
        }
        /*Calculates the loss chance based on how far through the play time range the current
         elapsed playtime is. This calculation is given less weighting as it was found through 
         testing that it strongly dictated the loss chance far more than the other calculations.*/

        else if (p.playTime > p.longestPlayTime) p.pr_loss = playTimeWeighting;
        /*If the player has surpassed their longest elapsed playtime, the loss chance is maximum.
         This calculation is laso given less weighting as it was found through testing that it 
         strongly dictated the loss chance far more than the other calculations.*/

        CalculateHealthGap();
    }


    private void CalculateHealthGap()
    {
        p.pr_loss += p.healthGap;
        //Adds health gap between player and enemy to the loss chance.

        if (p.pr_loss < 0) p.pr_loss = 0;

        else if (p.pr_loss > 1) p.pr_loss = 1;
        /*Apply bounds here to prevent skewing the next calculation if value is over 1 or below 0.
         This is necessary here as it is possible for the value to exceed its bounds at this point.*/

        CalculateAccuracies();
    }


    private void CalculateAccuracies()
    {
        if (p.playerHealth <= p.enemyDamage) p.pr_loss += p.enemyAccuracy; 

        if (p.enemyHealth <= p.playerDamage) p.pr_loss -= p.playerAccuracy; 
        /*If player or enemy are able to lose within one hit, apply the accuracy of their opponent
         to the loss chance.*/

        ApplyBoundsToLossChance();
    }


    private void ApplyBoundsToLossChance()
    {
        if (p.pr_loss < 0) p.pr_loss = 0;

        else if (p.pr_loss > 1) p.pr_loss = 1;
        //Apply bounds here to prevent skewing the final calculation if value is over 1 or below 0.

        AdjustDifficulty();
    }


    public override void AdjustDifficulty()
    {
        float difficulty = ((1 - p.pr_loss) * (maxDifficulty - minDifficulty)) + minDifficulty;
        
        p.difficulty = difficulty;
        /*Inversely apply the loss probability within a range (between maxDifficulty and minDifficulty)
         to the difficulty value.*/

        ChangeStaffColour(p.pr_loss);
        /*Aesthetic change for testing, displays difficulty from green (easiest) to red (hardest) 
         on the enemy's weapon. The lighting map is applied as a secondary texture on the enemy sprite
         and acts as an emission map, which is used by the custom material to give a glow effect to
         desired areas of the sprite.*/
    }


    private void ChangeStaffColour(float d)
    {
        enemyMat.color = new Color(255 - (d * 255), d * 255, 0);
        //Scales a colour from green (0,255,0) to red (255,0,0) with the difficulty value.
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