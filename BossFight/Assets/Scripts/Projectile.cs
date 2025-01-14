using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform tr;
    private float attackDamage, speed;
    private Health playerHealth;
    private bool canDamage;
    private Enemy_Attack attackState;
    private Enemy_Idle idleState;
    private Vector2 moveTo;
    private Enemy enemy;
    public GameObject slamPrefab;
    [SerializeField] private GameObject telegraphIndicator;
    public int detonateCount;
    [SerializeField] DynamicAdjuster d;
    private float detonationSizeMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        d = GameObject.Find("Dynamic Adjuster").GetComponent<DynamicAdjuster>();
        attackState = GameObject.Find("Attack").GetComponent<Enemy_Attack>();
        idleState = GameObject.Find("Idle").GetComponent<Enemy_Idle>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        moveTo = enemy.GetPlayerPos();
        tr = transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canDamage = true;
        attackDamage = d.dA.projectileDamage;
        speed = d.dA.projectileSpeed;
        PlayDataSingleton.instance.enemyAttacks++;
    }

    // Update is called once per frame
    void Update()
    {
        tr.position = Vector2.MoveTowards(transform.root.position, moveTo, speed * Time.deltaTime);

        //DAMAGE PLAYER
        if (this.GetComponent<CheckContainsPlayer>().containsPlayer && canDamage)
        {
            playerHealth.DamageFor(attackDamage, true);
            PlayDataSingleton.instance.enemyHits++;
            canDamage = false;
            Destroy(gameObject); //needs to be called when projectile exits view also
        }
    }

    public void Detonate()
    {
        //if(detonateCount < 1)
        //{
            StartCoroutine(WaitToDetonate());
            detonateCount++;
        //}
        //do
        //{
        //    Instantiate(telegraphIndicator, this.transform.position, Quaternion.identity);
        //    Debug.Log("blow up");
        //    detonateCount++;
        //} while (detonateCount < 1);
    }

    IEnumerator WaitToDetonate()
    {
        if(detonateCount < 1) Instantiate(telegraphIndicator, this.transform.position, Quaternion.identity);
        telegraphIndicator.transform.parent = transform;
        yield return new WaitForSeconds(d.dA.telegraphDetonateFor);
        slamPrefab.GetComponent<Slam>().size = d.dA.slamRadius * detonationSizeMultiplier;
        Instantiate(slamPrefab, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
