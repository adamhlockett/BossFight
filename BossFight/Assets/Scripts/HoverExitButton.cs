using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverExitButton : MonoBehaviour
{
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.Play("menubackgroundhoverexit");
        //canExit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.Play("menubackgroundidle");
        //canExit = false;
    }
}
