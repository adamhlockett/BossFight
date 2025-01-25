using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using Color = UnityEngine.Color;

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
    [SerializeField] SpriteRenderer tutorialText;
    [SerializeField] GameObject player;
    [SerializeField] GameObject playerStartPos;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemyStartPos;
    [SerializeField] DynamicAdjuster d;
    [SerializeField] TestInfoHider testInfoHider;
    [SerializeField] Material enemyMat;
    GameObject[] projectiles;
    GameObject[] slams;
    public float transitionLength = 1f;
    private float pauseKeyPresses;
    public bool isPaused = false, inTraining = true;
    private bool canLose = true, canWin = true, hasDoneTutorial = false;
    PlayDataSingleton p = PlayDataSingleton.instance;

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
        player.transform.position = playerStartPos.transform.position;
        enemy.transform.position = enemyStartPos.transform.position;
        playerHealth.SetHealth(playerHealth.maxhp, true);
        enemyHealth.SetHealth(enemyHealth.maxhp, false);
        playerHealth.UpdateLowHealthIndicator();
        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject p in projectiles) Destroy(p);
        slams = GameObject.FindGameObjectsWithTag("Slam");
        foreach (GameObject s in slams) Destroy(s);
        enemyFSM.Restart();
        d.ApplyInitialValues();
        tutorialText.color = new Color(255, 255, 255, 255);
        if (hasDoneTutorial) ExitTraining();
        p.playTime = 0;
        p.difficulty = 1;
        enemyMat.color = new Color(255 - (p.pr_loss * 255), p.pr_loss * 255, 0);
    }

    private void Update()
    {
        p.playTime += Time.deltaTime;
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
            if (inTraining)
            {
                if (!testInfoHider.hide)
                {
                    pauseKeyPresses++;
                    PauseAndPlay();
                }
            }
            else
            {
                pauseKeyPresses++;
                PauseAndPlay();
            }
        }
        if (isPaused)
        {
            if (Input.GetButtonDown("Retry"))
            {
                Play();
                p.retries++;
                Lose();
            }
            if (Input.GetButtonDown("Menu"))
            {
                Play();
                StartCoroutine(LoadThisScene("Menu"));
            }
        }
    }

    public void PauseAndPlay() 
    {
        if (inTraining) { TrainingSequence(); return; } // skips the rest of this method when in training

        if (pauseKeyPresses % 2 == 1) Pause();

        else if (pauseKeyPresses % 2 == 0) Play();
    }

    private void TrainingSequence()
    {
        if (pauseKeyPresses >= trainingPrompts.Length) { ExitTraining(); return; }
        if (pauseKeyPresses == trainingPrompts.Length - 1) trainingVideo.enabled = false;
        /*if((int)pauseKeyPresses <= trainingClips.Length)*/ trainingVp.clip = trainingClips[(int)pauseKeyPresses];
        trainingPrompt.sprite = trainingPrompts[(int)pauseKeyPresses];
    }

    public void Lose() 
    {
        if(p.playTime < p.shortestPlayTime) p.shortestPlayTime = p.playTime;
        if (p.playTime > p.longestPlayTime) p.longestPlayTime = p.playTime;
        p.totalPlayTime += p.playTime;
        p.attempts++;
        Gamepad.current.SetMotorSpeeds(0f, 0f);
        Restart(); // should just restart the level
    }

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
        tutorialText.color = new Color(255, 255, 255, 0);
        trainingInfo.SetActive(false);
        trainingVideo.enabled = false;
        enemyFSM.canChangeState = true;
        enemyHealth.canBeDamaged = true;
        canLose = true;
        canWin = true;
        pauseKeyPresses = 0;
        enemyFSM.Restart();
        player.transform.position = playerStartPos.transform.position;
        playerHealth.SetHealth(playerHealth.maxhp, true);
        playerHealth.UpdateLowHealthIndicator();
        hasDoneTutorial = true;
    }

    IEnumerator LoadThisScene(string sceneName)
    {
        //trigger animation start
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionLength);
        SceneManager.LoadScene(sceneName);
    }
}
