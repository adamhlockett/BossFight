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

            if (canMode) PlayDataSingleton.instance.playMode++;

            if (canExit) Quit();
        }

        if (PlayDataSingleton.instance.playMode > 2) PlayDataSingleton.instance.playMode = 0;

        if (PlayDataSingleton.instance.playMode != lastPlayMode) // do once
        {
            cursor.material = cursorMaterials[PlayDataSingleton.instance.playMode];
            lastPlayMode = PlayDataSingleton.instance.playMode;
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
