using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPlayButton : MonoBehaviour
{
    [SerializeField] Animator anim;
    float normalTime = 0f;
    [SerializeField] CheckMenuInputs inputChecker;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        normalTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("menubackgroundhoverplay", 0, normalTime);
        inputChecker.canPlay = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        normalTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        anim.Play("menubackgroundidle", 0, normalTime);
        inputChecker.canPlay = false;
    }
}
