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
    [SerializeField] DynamicAdjuster d;
    public float size;

    // Start is called before the first frame update
    void Start()
    {
        d = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
        chargeState = GameObject.Find("Charge").GetComponent<Enemy_Charge>();
        tr = transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canDamage = true;
        size = d.dA.slamRadius;
        tr.localScale *= size;
        slamDamage = d.dA.slamDamage;
        PlayDataSingleton.instance.enemyAttacks++;
    }

    // Update is called once per frame
    void Update()
    {
        tr.localScale *= 1 + (scaleBy * Time.deltaTime);

        //DAMAGE PLAYER
        if (this.GetComponent<CheckContainsPlayer>().containsPlayer && canDamage)
        {
            playerHealth.DamageFor(slamDamage, true);
            PlayDataSingleton.instance.enemyHits++;
            canDamage = false;
            StartCoroutine(DamageCooldown());
        }
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(d.dA.damageRate);
        canDamage = true;
    }

    private void DestroyThis() //used in animation event
    {
        Destroy(gameObject);
    }
}
