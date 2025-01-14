using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMenuInputs : MonoBehaviour
{
    public bool canPlay = false, canMode = false, canExit = false;
    private int lastPlayMode = 0;
    [SerializeField] Material[] cursorMaterials;
    [SerializeField] SpriteRenderer cursor;

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (canPlay) SceneManager.LoadScene("BossFight");

            if (canMode) PlayModeSingleton.instance.playMode++;

            if (canExit) Quit();
        }

        if (PlayModeSingleton.instance.playMode > 2) PlayModeSingleton.instance.playMode = 0;

        if (PlayModeSingleton.instance.playMode != lastPlayMode) // do once
        {
            cursor.material = cursorMaterials[PlayModeSingleton.instance.playMode];
            lastPlayMode = PlayModeSingleton.instance.playMode;
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
