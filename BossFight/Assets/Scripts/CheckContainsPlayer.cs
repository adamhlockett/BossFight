using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckContainsPlayer : MonoBehaviour
{
    public bool containsPlayer = false;
    public Health playerHealth;
    public Transform playerPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Hitbox")
        {
            containsPlayer = true;
            playerHealth = collision.gameObject.GetComponentInParent<Health>();
            playerPos = collision.gameObject.GetComponentInParent<Transform>();
            //Debug.Log("hit");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Hitbox")
        {
            containsPlayer = false;
        }
    }
}
