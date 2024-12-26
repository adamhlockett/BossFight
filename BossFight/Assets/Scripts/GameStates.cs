using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStates : MonoBehaviour
{
    [SerializeField] Health enemyHealth;
    [SerializeField] Health playerHealth;
    private float pauseKeyPresses;
    private bool canLose = true, canWin = true;

    // initial setup
    private void Restart() // call this when game is restarted AND when it is started initially (probably in Enemy)
    {
        // enemy cannot leave idle state nor take damage
        canLose = false;
        canWin = false;
    }


    private void LateUpdate()
    {
        CheckConditions();
        CheckInputs();
    }

    private void CheckConditions()
    {
        if (playerHealth.IsDead()) { Lose(); }
        if (enemyHealth.IsDead()) { Win(); }
    }

    private void CheckInputs()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pauseKeyPresses++;
            PauseAndPlay();
        }
    }

    // if player presses PAUSE KEY the first time, allow PLAY - if player presses PAUSE KEY after the first time, pause game
    public void PauseAndPlay() 
    {;
        //switch (pauseKeyPresses){
        //    case 0:
        //        return;
        //    case 1:
        //        //enemy can act and take damage
        //        return;
        //    case (pauseKeyPresses % 2 == 0):
        //        //even number
        //        return;
        //}

        if (pauseKeyPresses == 0) { return; }

        else if (pauseKeyPresses == 1)
        {
            BeginGameLoop();
            return;
        }
        else if (pauseKeyPresses % 2 == 0) // even number
        {
            pauseKeyPresses = 2;
            Pause();
            return;
        }
        else if (pauseKeyPresses % 2 == 1) // odd number
        {
            pauseKeyPresses = 3;
            Play();
            return;
        }
        // FIRST PRESS - enemy can act and take damage
        // SUBSEQUENT PRESSES - PAUSE/PLAY game (can figure this out with a press-counter that checked if EVEN or ODD)
    }

    // if player is dead, move to loss screen
    public void Lose() 
    {
        Debug.Log("lose");
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        SceneManager.LoadScene("Lose");
    }

    // if enemy is dead, move to win screen
    public void Win()
    {
        Debug.Log("win");
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        SceneManager.LoadScene("Win");
    }

    public void Pause()
    {
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        Time.timeScale = 1f;
    }

    public void BeginGameLoop()
    {
        // call method on enemy that allows it to act and take damage
        canLose = true;
        canWin = true;
    }
}
