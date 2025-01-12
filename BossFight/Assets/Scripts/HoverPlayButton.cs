using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPlayButton : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.Play("menubackgroundhoverplay");
        //canPlay = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        anim.Play("menubackgroundidle");
        //canPlay = false;
    }
}
