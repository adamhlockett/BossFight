using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    DynamicAdjuster d;
    //GameObject player;
    //Transform enemyPos;
    int tempAngle;
    public bool playerHasChangedSide = false;
    GameObject[] projectiles;
    public Enemy_Attack attackRef;

    private void Start()
    {
        //enemyPos = transform;
        //playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        d = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
    }

    private void Update()
    {
        //if (enemyPos == null) enemyPos = transform;
        //player = GameObject.FindGameObjectWithTag("Player");
        if (GetPlayerAngleFromEnemy() != tempAngle) playerHasChangedSide = true;
        else playerHasChangedSide = false;
        tempAngle = GetPlayerAngleFromEnemy();

        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        if(projectiles.Length >= attackRef.fireAmount * d.dA.detonateAfterTurns)
        {
            projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject projectile in projectiles) 
            {
                projectile.GetComponent<Projectile>().Detonate();
            }
        }
    }

    public Vector2 GetPlayerPos() { return GameObject.FindGameObjectWithTag("Player").transform.position; }

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
        float angle = Mathf.Atan2(transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x) * 180 / Mathf.PI;
        int angleInt = ((int)(angle / 45));
        return angleInt;
    }
}
