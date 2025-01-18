using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool isWaiting;
    GameStates gameStates;

    private void Awake()
    {
        gameStates = GameObject.Find("GameStateHandler").GetComponent<GameStates>();
    }

    public void StopFor(float duration)
    {
        if (isWaiting || gameStates.isPaused) return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        isWaiting = true;
        yield return new WaitForSecondsRealtime(duration);
        if(!gameStates.isPaused) Time.timeScale = 1.0f;
        isWaiting = false;
    }
}
