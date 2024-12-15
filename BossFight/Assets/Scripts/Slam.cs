using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    Transform tr;
    float scaleBy = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.localScale *= 1 + (scaleBy * Time.deltaTime);

        //DAMAGE PLAYER
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}
