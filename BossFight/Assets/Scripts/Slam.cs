using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slam : MonoBehaviour
{
    Transform tr;
    float scaleBy = 0.1f;
    float slamDamage;
    Health playerHealth;
    bool canDamage;
    Enemy_Charge chargeState;

    // Start is called before the first frame update
    void Start()
    {
        chargeState = GameObject.Find("Charge").GetComponent<Enemy_Charge>();
        tr = transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canDamage = true;
        tr.localScale *= chargeState.slamPrefabSize;
        slamDamage = chargeState.damage;
    }

    // Update is called once per frame
    void Update()
    {
        tr.localScale *= 1 + (scaleBy * Time.deltaTime);

        //DAMAGE PLAYER
        if (this.GetComponent<CheckContainsPlayer>().containsPlayer && canDamage)
        {
            playerHealth.DamageFor(slamDamage, true);
            canDamage = false;
            StartCoroutine(damageCooldown());
        }
    }

    private IEnumerator damageCooldown()
    {
        yield return new WaitForSeconds(chargeState.canDamageEvery);
        canDamage = true;
    }

    private void DestroyThis() //used in animation event
    {
        Destroy(gameObject);
    }
}
