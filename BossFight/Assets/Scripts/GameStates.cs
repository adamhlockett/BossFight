using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStates : MonoBehaviour
{
    public Health enemyHealth;
    public Health playerHealth;
    private float pauseKeyPresses;

    // initial setup
    private void Restart() // call this when game is restarted and when it is started initially (probably in Enemy)
    {
        // enemy cannot leave idle state nor take damage
    }

    private void LateUpdate()
    {
        CheckConditions();
        CheckInputs();
    }

    private void CheckConditions()
    {
        //if player is dead then lose
        //if enemy is dead then win
    }

    private void CheckInputs()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pauseKeyPresses++;
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

        if (pauseKeyPresses == 0)
        {
            return;
        }
        else if (pauseKeyPresses == 1)
        {
            // call method on enemy that allows it to act and take damage
            return;
        }
        else if (pauseKeyPresses % 2 == 0) // even number
        {
            pauseKeyPresses = 2;
            // pause game
        }
        else if (pauseKeyPresses % 2 == 1) // odd number
        {
            pauseKeyPresses = 3;
            // play game
        }
        // FIRST PRESS - enemy can act and take damage
        // SUBSEQUENT PRESSES - PAUSE/PLAY game (can figure this out with a press-counter that checked if EVEN or ODD)
    }

    // if player is dead, move to loss screen
    public void Lose() 
    { 
        //if(playerHealth.)
    }

    // if enemy is dead, move to win screen
    public void Win()
    {
    
    }
}
