using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour //abstract means cannot be used, can only use classes that inherit from this
{
    public abstract State RunCurrentState();

    public abstract void OnEnter(Animator anim);
}
