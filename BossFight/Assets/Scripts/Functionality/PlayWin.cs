using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayWin : MonoBehaviour
{
    private void Start() { Time.timeScale = 1f; PlayWinScreen(); }

    private void PlayWinScreen()
    {

    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
