using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    Transform enemyPos;
    int tempAngle;
    public bool playerHasChangedSide = false;

    private void Start()
    {
        enemyPos = transform;
    }

    private void Update()
    {
        if (GetPlayerAngleFromEnemy() != tempAngle) playerHasChangedSide = true;
        else playerHasChangedSide = false;
        tempAngle = GetPlayerAngleFromEnemy();
    }

    public int GetPlayerAngleFromEnemy()
    {
        float angle = Mathf.Atan2(enemyPos.position.y - playerPos.position.y, enemyPos.position.x - playerPos.position.x) * 180 / Mathf.PI;
        int angleInt = ((int)(angle / 45));
        return angleInt;
    }

    public string GetEnemyShouldFace()
    {
        switch (GetPlayerAngleFromEnemy())
        {
            case 0:
                return "LEFT";
            case 1:
                return "DOWN";
            case 2:
                return "DOWN";
            case 3:
                return "RIGHT";
            case -3:
                return "RIGHT";
            case -2:
                return "UP";
            case -1:
                return "UP";
            default:
                return "DOWN";
        }
    }
}
