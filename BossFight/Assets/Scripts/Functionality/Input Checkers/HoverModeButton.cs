using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverModeButton : MonoBehaviour
{
    [SerializeField] Animator anim;
    float normalTime = 0f;
    [SerializeField] CheckMenuInputs inputChecker;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        normalTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("menubackgroundhovermode", 0, normalTime);
        inputChecker.canMode = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        normalTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("menubackgroundidle", 0, normalTime);
        inputChecker.canMode = false;
    }
}
