using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum PlayModes
{
    Control = 0,
    Probabilistic = 1,
    MCTS = 2
}


public class CheckMenuInputs : MonoBehaviour
{
    public bool canPlay = false, canMode = false, canExit = false;
    public int playMode = 0;
    private int lastPlayMode = 0;
    [SerializeField] Material[] cursorMaterials;
    [SerializeField] SpriteRenderer cursor;

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (canPlay) SceneManager.LoadScene("BossFight");

            if (canMode) playMode++;

            if (canExit) Quit();
        }

        if (playMode > 2) playMode = 0;

        if (playMode != lastPlayMode) // do once
        {
            cursor.material = cursorMaterials[playMode];
            lastPlayMode = playMode;
        }
    }

    public void Quit() //method from stack overflow https://stackoverflow.com/questions/70437401/cannot-finish-the-game-in-unity-using-application-quit
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
