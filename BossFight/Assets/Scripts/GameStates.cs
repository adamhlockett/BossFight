using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameStates : MonoBehaviour
{
    [SerializeField] Health enemyHealth;
    [SerializeField] Health playerHealth;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject trainingInfo;
    [SerializeField] EnemyStateMachine enemyFSM;
    [SerializeField] Animator transitionAnim;
    [SerializeField] VideoPlayer trainingVp;
    [SerializeField] SpriteRenderer trainingPrompt;
    [SerializeField] VideoClip[] trainingClips;
    [SerializeField] Sprite[] trainingPrompts;
    [SerializeField] RawImage trainingVideo;
    public float transitionLength = 1f;
    private float pauseKeyPresses;
    public bool isPaused = false;
    private bool canLose = true, canWin = true, inTraining = true;

    private void Start()
    {
        Restart();
    }

    public void Restart() // call when game is restarted
    {
        inTraining = true;
        trainingInfo.SetActive(true);
        trainingVp.clip = trainingClips[0];
        trainingPrompt.sprite = trainingPrompts[0];
        enemyFSM.canChangeState = false;
        enemyHealth.canBeDamaged = false;
        canLose = false;
        canWin = false;
        pauseKeyPresses = 0;
        trainingVideo.enabled = true;
        //should contain starting positions for player and enemy
    }

    private void LateUpdate()
    {
        CheckConditions();
        CheckInputs();
    }

    private void CheckConditions()
    {
        if (playerHealth.IsDead() && canLose) { Lose(); }
        if (enemyHealth.IsDead() && canWin) { Win(); }
    }

    private void CheckInputs()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pauseKeyPresses++;
            PauseAndPlay();
        }
    }

    public void PauseAndPlay() 
    {
        if (inTraining) { TrainingSequence(); return; } // skips the rest of this method when in training

        if (pauseKeyPresses % 2 == 1) Pause();

        else if (pauseKeyPresses % 2 == 2) Play();
    }

    private void TrainingSequence()
    {
        if (pauseKeyPresses >= trainingPrompts.Length) { ExitTraining(); return; }
        if (pauseKeyPresses == trainingPrompts.Length - 1) trainingVideo.enabled = false;
        /*if((int)pauseKeyPresses <= trainingClips.Length)*/ trainingVp.clip = trainingClips[(int)pauseKeyPresses];
        trainingPrompt.sprite = trainingPrompts[(int)pauseKeyPresses];
    }

    // if player is dead, move to loss screen
    public void Lose() 
    {
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        StartCoroutine(LoadThisScene("Lose")); // should just restart the level
    }

    // if enemy is dead, move to win screen
    public void Win()
    {
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        StartCoroutine(LoadThisScene("Win"));
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
        inTraining = false;
        trainingInfo.SetActive(false);
        enemyFSM.canChangeState = true;
        enemyHealth.canBeDamaged = true;
        canLose = true;
        canWin = true;
        pauseKeyPresses = 0;
    }

    IEnumerator LoadThisScene(string sceneName)
    {
        //trigger animation start
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionLength);
        SceneManager.LoadScene(sceneName);
    }
}
