using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContainsEnemy : MonoBehaviour
{
    public bool containsEnemy = false;
    public Health enemyHealth;
    public Transform enemyPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy Hitbox")
        {
            containsEnemy = true;
            enemyHealth = collision.gameObject.GetComponentInParent<Health>();
            enemyPos = collision.gameObject.GetComponentInParent<Transform>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy Hitbox")
        {
            containsEnemy = false;
        }
    }
}
