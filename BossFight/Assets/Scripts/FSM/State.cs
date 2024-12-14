using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour //abstract means cannot be used, can only use classes that inherit from this
{
    public abstract State RunCurrentState(Animator anim, Enemy enemy);

    public abstract void OnEnter(Animator anim, Enemy enemy);

    public abstract void StartAnim(Animator anim, Enemy enemy);
}
