using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataSingleton : MonoBehaviour
{
    public static PlayDataSingleton instance { get; private set; }

    public int playMode;
//probabilistic method data
    public float
/*trajectories*/attempts, retries, 
        playerHealth, playerMaxHealth, enemyHealth, enemyMaxHealth, healthGap, 
        playerAttacks, playerHits, playerAccuracy, 
        enemyAttacks, enemyHits, enemyAccuracy,
        playTime, avgPlayTime, totalPlayTime, shortestPlayTime, longestPlayTime,
/*probabilities*/pr_loss, pr_playerHit, pr_enemyHit;
    //public List<float> playTimes = new List<float>();

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);

        else instance = this;
    }
}
