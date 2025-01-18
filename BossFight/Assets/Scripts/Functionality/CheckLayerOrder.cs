using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLayerOrder : MonoBehaviour
{
    [SerializeField] Transform playerTr;
    [SerializeField] GameObject enemy;
    private Transform enemyTr;
    private SpriteRenderer enemySprite;

    private void Start()
    {
        enemyTr = enemy.GetComponent<Transform>();
        enemySprite = enemy.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (playerTr.position.y > enemyTr.position.y)
        {
            enemySprite.sortingOrder = 3;
        }
        else enemySprite.sortingOrder = 1;
    }
}
