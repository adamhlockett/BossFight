using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataSingleton : MonoBehaviour
{
    public static PlayDataSingleton instance { get; private set; }

    public int playMode;
    public float
//probabilistic method data
/*trajectories*/attempts, retries, 
        playerHealth, playerMaxHealth, enemyHealth, enemyMaxHealth, healthGap, 
        playerAttacks, playerHits, playerAccuracy, playerDamage, 
        enemyAttacks, enemyHits, enemyAccuracy, enemyDamage,
        playTime, avgPlayTime, totalPlayTime, shortestPlayTime, longestPlayTime,
/*probabilities*/pr_loss, pr_playerHit, pr_enemyHit,
//monte carlo tree search data
        chargeAttacks, chargeHits, chargeAccuracy,
        attackAttacks, attackHits, attackAccuracy,
//difficulty
        difficulty;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);

        else instance = this;
    }

    private void Update()
    {
        Debug.Log(attackAttacks);
    }
}
