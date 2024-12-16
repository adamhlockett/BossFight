using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    Transform tr;
    float scaleBy = 0.1f;
    public float slamDamage;
    Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.localScale *= 1 + (scaleBy * Time.deltaTime);

        //DAMAGE PLAYER
        if (this.GetComponent<CheckContainsPlayer>().containsPlayer)
        {
            playerHealth.DamageFor(slamDamage);
            //start damage cooldown
        }
    }

    private void DestroyThis() //used in animation event
    {
        Destroy(gameObject);
    }
}
