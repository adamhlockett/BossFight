using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataSingleton : MonoBehaviour
{
    public static PlayDataSingleton instance { get; private set; }

    public int playMode;
//probabilistic method data
    public float
/*trajectories*/losses, retries, playerHealth, playerMaxHealth, enemyHealth, enemyMaxHealth, healthGap, playerAttacks, playerHits, playerAccuracy, enemyAttacks, enemyHits, enemyAccuracy,
/*probabilities*/pr_loss, pr_playerHit, pr_enemyHit, pr_retry;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);

        else instance = this;
    }
}
