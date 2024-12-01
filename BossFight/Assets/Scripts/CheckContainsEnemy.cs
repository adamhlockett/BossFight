using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContainsEnemy : MonoBehaviour
{
    public bool containsEnemy = false;
    public Health enemyHealth;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            containsEnemy = true;
            enemyHealth = collision.gameObject.GetComponentInParent<Health>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            containsEnemy = false;
        }
    }
}
