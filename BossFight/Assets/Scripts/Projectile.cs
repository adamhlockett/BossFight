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
    private Vector2 moveTo;
    private Enemy enemy;
    public GameObject slamPrefab;

    // Start is called before the first frame update
    void Start()
    {
        attackState = GameObject.Find("Attack").GetComponent<Enemy_Attack>();
        enemy = GameObject.Find("Boss").GetComponent<Enemy>();
        moveTo = enemy.GetPlayerPos();
        tr = transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canDamage = true;
        attackDamage = attackState.damage;
        speed = attackState.speed;
    }

    // Update is called once per frame
    void Update()
    {
        tr.position = Vector2.MoveTowards(transform.root.position, moveTo, speed * Time.deltaTime);

        //DAMAGE PLAYER
        if (this.GetComponent<CheckContainsPlayer>().containsPlayer && canDamage)
        {
            playerHealth.DamageFor(attackDamage, true);
            canDamage = false;
            Destroy(gameObject); //needs to be called when projectile exits view also
        }
    }

    public void Detonate()
    {
        //instantiate a slam
        Instantiate(slamPrefab, this.transform.position, Quaternion.identity);
        Debug.Log("detonate");
        Destroy(gameObject);
    }
}
