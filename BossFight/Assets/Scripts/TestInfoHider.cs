using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class TestInfoHider : MonoBehaviour
{
    public GameObject player;
    [SerializeField] RawImage video;
    [SerializeField] GameObject moveTo;
    [SerializeField] GameObject moveFrom;
    [SerializeField] GameObject videoMoveTo;
    [SerializeField] GameObject videoMoveFrom;
    [SerializeField] GameObject videoGo;
    public bool hide = false, isWaiting = false;
    public float speed = 5f;

    private void Update()
    {
        if (hide)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveTo.transform.position, step);
            videoGo.transform.position = Vector3.MoveTowards(videoGo.transform.position, videoMoveTo.transform.position, step);
        }
        else
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveFrom.transform.position, step);
            videoGo.transform.position = Vector3.MoveTowards(videoGo.transform.position, videoMoveFrom.transform.position, step);
        }
    }

    public IEnumerator SwapMove(bool p_movingOut)
    {
        isWaiting = true;
        yield return new WaitForSeconds(0.5f);
        hide = !p_movingOut;
        isWaiting = false;
    }

    //private void Out()
    //{
    //    //GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.2f);
    //    //video.color = new Color(255, 255, 255, 0.5f);
    //}
    //
    //private void In()
    //{
    //    //GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
    //    //video.color = new Color(255, 255, 255, 1f);
    //}
}
