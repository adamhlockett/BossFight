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
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject testInfo;
    [SerializeField] EnemyStateMachine enemyFSM;
    private float pauseKeyPresses;
    private bool canLose = true, canWin = true;
    public bool isPaused = false;

    private void Start()
    {
        Restart();
    }

    public void Restart() // call this when game is restarted
    {
        enemyHealth.canBeDamaged = false;
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
    {
        if (pauseKeyPresses == 0) { return; }

        else if (pauseKeyPresses == 1)
        {
            ExitTraining();
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
        isPaused = true;
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Play()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitTraining()
    {
        testInfo.SetActive(false);
        enemyFSM.canChangeState = true;
        enemyHealth.canBeDamaged = true;
        canLose = true;
        canWin = true;
    }
}
