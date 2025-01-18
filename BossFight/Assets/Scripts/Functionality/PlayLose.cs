using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayLose : MonoBehaviour
{
    private void Start() { Time.timeScale = 1f; PlayLoseScreen(); }
    private void PlayLoseScreen() 
    {
        GetComponent<Animator>().Play("playthrough");
    }
}
