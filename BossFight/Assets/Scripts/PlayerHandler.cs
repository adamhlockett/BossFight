using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input, lastMoveDirection, moves;
    Animator anim;
    [SerializeField] float startSpeed; //set in engine inspector
    private float speed;

    [Header("Dashing")]
    private bool canDash = true, isDashing;
    [SerializeField] float dashDistance; //set in engine inspector
    private float dashTime = 0.2f, dashCooldown = 0.5f;

    [Header("Attacking")]
    private bool canAtk = true /*,isAttacking = false*/;
    [SerializeField] float atkDamage, atkDistanceMax; //set in engine inspector
    private float atkTime = 0.2f, atkCooldown = 0.2f, startAtkDistance = 1f, atkDistance, atkTimerMultiplier, atkAnimRunoff = 0.15f;
    [SerializeField] Transform attackZone;

    [Header("Visual Feedback")]
    [SerializeField] Camera cam;
    [SerializeField] GameObject damagePopupPrefab;
    [SerializeField] GameObject attackLinePrefab;
    Transform enemyPos;
    private float shakeFor = 0f, shakeBy = 0.02f, startShakeBy, decrementBy = 4f, maxShakeFor = 0.5f;
    public float hitStopDuration = 0.1f;



    void Start()
    {
        speed = startSpeed;
        atkDistance = startAtkDistance;
        atkTimerMultiplier = atkDistanceMax * 0.7f;
        startShakeBy = shakeBy;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Animations
        ManageAnims();

        //Inputs
        ManageInputs();

        //Attack
        ManageAttack();

        //Visual Feedback
        ManageVisualFeedback();

    }

    private void FixedUpdate()
    {
        rb.velocity = input * speed; //move player
    }



    void ManageInputs()
    {
        if (isDashing) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moves = new Vector2(moveX, moveY);

        float angle = Mathf.Atan2(moveX, moveY) * Mathf.Rad2Deg;                       //rotate attack zone with joystick input
        attackZone.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));             //rotate attack zone with joystick input

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0)) lastMoveDirection = input;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash(dashDistance));
        }
    }

    void ManageAnims()
    {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", moves.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void ManageAttack()
    {
        if (Input.GetButtonUp("Attack") && canAtk)                                                                  //release attack
        {                                                                                                           //release attack
            speed = startSpeed; //can move again once releasing attack                                              //release attack
            anim.SetTrigger("Attack");                                                                              //release attack
            StartCoroutine(Attack(atkDamage * atkDistance));                                                        //release attack
        }                                                                                                           //release attack

        if (Input.GetButton("Attack") && canAtk)                                                                    //hold attack
        {                                                                                                           //hold attack
            anim.SetTrigger("ChargingAttack"); //should ideally be a boolean to be more efficient                   //hold attack
            speed = 0f; //cant move while charging attack                                                           //hold attack
                                                                                                                    //hold attack
            if (atkDistance <= atkDistanceMax)                                                                      //hold attack
            {                                                                                                       //hold attack
                atkDistance += Time.deltaTime * atkTimerMultiplier; //increases atkPower by 2 every second          //hold attack
            }                                                                                                       //hold attack
            else                                                                                                    //hold attack
            {                                                                                                       //hold attack
                atkDistance = atkDistanceMax;                                                                       //hold attack
                shakeFor = maxShakeFor;                                                                             //hold attack
            }                                                                                                       //hold attack
        }                                                                                                           //hold attack

        //if(isAttacking && attackZone.gameObject.GetComponent<CheckContainsEnemy>().containsEnemy) //do once
        //{
        //    Debug.Log("hitting enemy");
        //    isAttacking = false;
        //    attackZone.gameObject.GetComponent<CheckContainsEnemy>().enemyHealth.DamageFor(atkDamage);
        //}
    }


    private IEnumerator Dash(float power)
    {
        canDash = false;
        isDashing = true;

        speed = power; //during dash

        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        speed = startSpeed;
        yield return new WaitForSeconds(dashCooldown);
        if(!canDash) canDash = true;
    }

    private IEnumerator Attack(float damage)
    {
        canAtk = false;
        //isAttacking = true;

        StartCoroutine(Dash(atkDistance)); //during attack

        yield return new WaitForSeconds(atkTime);

        if (attackZone.gameObject.GetComponent<CheckContainsEnemy>().containsEnemy)
        {
            attackZone.gameObject.GetComponent<CheckContainsEnemy>().enemyHealth.DamageFor(damage);                                 //damage enemy

            enemyPos = attackZone.gameObject.GetComponent<CheckContainsEnemy>().enemyPos;                                           //spawn damage number
            GameObject currentDamagePopup = Instantiate(damagePopupPrefab, enemyPos.position, Quaternion.identity) as GameObject;   
            currentDamagePopup.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
            currentDamagePopup.GetComponent<TMPro.TextMeshProUGUI>().text = (Convert.ToInt32(damage)).ToString();

            //GameObject currentAttackLine = Instantiate(attackLinePrefab, enemyPos.position, Quaternion.identity) as GameObject;
            //currentAttackLine.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;

            //var points = new Vector3[2];
            //Vector3 midpoint = new Vector3(this.transform.position.x + (enemyPos.position.x - this.transform.position.x) * 0.5f,
            //                               this.transform.position.y + (enemyPos.position.y - this.transform.position.y) * 0.5f, 0);
            //
            //points[0] = midpoint//currentAttackLine.transform.position;
            //points[1] = //new Vector3(currentAttackLine.transform.position.x, currentAttackLine.transform.position.y - 1, currentAttackLine.transform.position.z + 1);
            //points[2] = midpoint + new Vector3()
            //    currentAttackLine.GetComponent<LineRenderer>().SetPositions(points);

            FindObjectOfType<HitStop>().StopFor(hitStopDuration);                                                                   //hitstop
            shakeBy *= 0.2f;
            shakeFor = maxShakeFor * 0.1f;
        }
        //if (isAttacking) isAttacking = false;
        //yield return new WaitForSeconds(atkAnimRunoff);
        anim.SetTrigger("HasAttacked");
        canDash = true;

        yield return new WaitForSeconds(atkCooldown);
        atkDistance = startAtkDistance;
        canAtk = true;
        shakeBy = startShakeBy;
    }

    private void ManageVisualFeedback()
    {
        if (shakeFor > 0)
        {
            cam.transform.localPosition = new Vector3 ( UnityEngine.Random.insideUnitSphere.x * shakeBy, UnityEngine.Random.insideUnitSphere.y * shakeBy, cam.transform.localPosition.z );
            shakeFor -= Time.deltaTime * decrementBy;
        }
        else shakeFor = 0f;
    }
}
