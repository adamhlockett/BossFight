using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    Transform tr;
    float scaleBy = 0.1f;
    public float slamDamage;
    Health playerHealth;
    bool canDamage;

    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        tr.localScale *= 1 + (scaleBy * Time.deltaTime);

        //DAMAGE PLAYER
        if (this.GetComponent<CheckContainsPlayer>().containsPlayer && canDamage)
        {
            playerHealth.DamageFor(slamDamage, true);
            //start damage cooldown
            canDamage = false;
            //start coroutine
            StartCoroutine(damageCooldown());
        }
    }

    private IEnumerator damageCooldown()
    {
        yield return new WaitForSeconds(2);
        canDamage = true;
    }

    private void DestroyThis() //used in animation event
    {
        Destroy(gameObject);
    }
}
