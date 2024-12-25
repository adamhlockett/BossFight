using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour //abstract means cannot be used, can only use classes that inherit from this
{
    public string stateName;
    public GameObject telegraphIndicator;

    public abstract void RunCurrentState(Animator anim, Enemy enemy);

    public abstract void OnEnter(Animator anim, Enemy enemy);

    public abstract void StartAnim(Animator anim, Enemy enemy);

    public abstract bool CheckIfComplete();
}
