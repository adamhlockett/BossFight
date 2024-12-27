using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] float speed; // set in editor
    private void Update()
    {   
        transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
    }

    public void DestroyThis() // used at end of animation
    {
        //fade out
        this.gameObject.GetComponent<SpriteRenderer>().color =
            new Color(this.gameObject.GetComponent<SpriteRenderer>().color.r,
            this.gameObject.GetComponent<SpriteRenderer>().color.g,
            this.gameObject.GetComponent<SpriteRenderer>().color.b,
            this.gameObject.GetComponent<SpriteRenderer>().color.a - (speed * 100 * Time.deltaTime));

        if (this.gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            Destroy(gameObject);
        }
        //StartCoroutine(WaitToDestroy());
    }

    //IEnumerator WaitToDestroy()
    //{
    //    yield return new WaitForSeconds(2);
    //    Destroy(gameObject);
    //}
}
