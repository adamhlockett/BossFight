using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameStates : MonoBehaviour
{
    private Health enemyHealth;
    private Health playerHealth;

    // initial setup
    private void Start()
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

    }

    // if player presses PAUSE KEY the first time, allow PLAY - if player presses PAUSE KEY after the first time, pause game
    void PauseAndPlay() 
    {
        // FIRST PRESS - enemy can act and take damage
        // SUBSEQUENT PRESSES - PAUSE/PLAY game (can figure this out with a press-counter that checked if EVEN or ODD)
    }

    // if player is dead, move to loss screen
    void Lose() 
    { 
    
    }

    // if enemy is dead, move to win screen
    void Win()
    {
    
    }
}
