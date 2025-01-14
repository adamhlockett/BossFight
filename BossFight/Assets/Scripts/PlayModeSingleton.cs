using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeSingleton : MonoBehaviour
{
    public static PlayModeSingleton instance { get; private set; }
    public int playMode;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);

        else instance = this;
    }
}
