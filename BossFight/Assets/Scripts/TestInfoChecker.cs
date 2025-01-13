using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInfoChecker : MonoBehaviour
{
    [SerializeField] TestInfoHider tih;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == tih.player && !tih.isWaiting) StartCoroutine(tih.SwapMove(false));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == tih.player && !tih.isWaiting) StartCoroutine(tih.SwapMove(true));
    }
}
