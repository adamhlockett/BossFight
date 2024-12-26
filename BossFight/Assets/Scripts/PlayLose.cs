using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayLose : MonoBehaviour
{
    private void Start() { Time.timeScale = 1f; PlayLoseScreen(); }
    public void PlayLoseScreen() 
    {
        GetComponent<Animator>().Play("playthrough");
        Debug.Log("play anim");
    }
}
