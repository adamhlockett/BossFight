using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    Transform enemyPos;
    int tempAngle;
    public bool playerHasChangedSide = false;
    GameObject[] projectiles;
    public Enemy_Attack attackRef;
    public float turnsBeforeDetonation = 3;

    private void Start()
    {
        enemyPos = transform;
    }

    private void Update()
    {
        if (GetPlayerAngleFromEnemy() != tempAngle) playerHasChangedSide = true;
        else playerHasChangedSide = false;
        tempAngle = GetPlayerAngleFromEnemy();

        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        if(projectiles.Length >= attackRef.fireAmount * turnsBeforeDetonation)
        {
            foreach (GameObject projectile in projectiles) 
            {
                projectile.GetComponent<Projectile>().Detonate();
            }
        }
    }

    public Vector2 GetPlayerPos() { return playerPos.position; }

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
    public int GetPlayerAngleFromEnemy()
    {
        float angle = Mathf.Atan2(enemyPos.position.y - playerPos.position.y, enemyPos.position.x - playerPos.position.x) * 180 / Mathf.PI;
        int angleInt = ((int)(angle / 45));
        return angleInt;
    }
}
