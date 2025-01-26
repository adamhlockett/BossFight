using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLayerOrder : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    GameObject enemy;
    private Transform enemyTr;
    private SpriteRenderer enemySprite;

    private void Start()
    {
        Restart();
    }

    private void Restart()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyTr = enemy.GetComponent<Transform>();
        enemySprite = enemy.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(enemy == null) { Restart(); }

        if (playerTr.position.y > enemyTr.position.y)
        {
            enemySprite.sortingOrder = 3;
        }
        else enemySprite.sortingOrder = 1;
    }
}
