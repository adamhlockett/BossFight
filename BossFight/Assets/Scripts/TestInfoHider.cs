using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInfoHider : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) Dim();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player) Light();
    }

    private void Dim()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.2f);
    }

    private void Light()
    {
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
    }
}
