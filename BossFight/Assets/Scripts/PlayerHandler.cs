using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input, lastMoveDirection, moves;
    Animator anim;
    [SerializeField] float startSpeed; //set in engine inspector
    private float speed;

    [Header("Dashing")]
    [SerializeField] float dashDistance; //set in engine inspector
    private bool canDash = true, isDashing;
    private float dashTime = 0.2f, dashCooldown = 0.5f;

    [Header("Attacking")]
    [SerializeField] float atkDamage, atkHoldTime; //set in engine inspector
    [SerializeField] Transform attackZone;
    private bool canAtk = true;
    private float atkTime = 0.2f, atkCooldown = 0.2f, startAtkDistance = 1f, atkDistance, atkTimerMultiplier, atkAnimRunoff = 0.15f, atkDistanceMax = 10f;

    [Header("Visual Feedback")]
    [SerializeField] Camera cam;
    [SerializeField] GameObject damagePopupPrefab;
    [SerializeField] GameObject attackLinePrefab;
    private float shakeFor = 0f, shakeBy = 0.02f, startShakeFor, startShakeBy, decrementBy = 4f, maxShakeFor = 0.5f;
    public float hitStopDuration = 0.1f;

    void Start()
    {
        speed = startSpeed;
        atkDistance = startAtkDistance;
        atkTimerMultiplier = atkDistanceMax * atkHoldTime;
        startShakeFor = shakeFor;
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
                ChargedShake();                                                                                     //hold attack
            }                                                                                                       //hold attack
        }                                                                                                           //hold attack
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

        StartCoroutine(Dash(atkDistance)); //during attack

        yield return new WaitForSeconds(atkTime);

        if (attackZone.gameObject.GetComponent<CheckContainsEnemy>().containsEnemy)
        {
            attackZone.gameObject.GetComponent<CheckContainsEnemy>().enemyHealth.DamageFor(damage, false);

            HitShake();
        }

        anim.SetTrigger("HasAttacked");
        canDash = true;

        yield return new WaitForSeconds(atkCooldown);
        atkDistance = startAtkDistance;
        canAtk = true;
        ResetShake();
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

    ////need input handler class

    public void SpawnDamagePopup(float damage, Transform spawnPos, bool isPlayer) // should ideally be in seperate script
    {
        GameObject currentDamagePopup = Instantiate(damagePopupPrefab, spawnPos.position, Quaternion.identity) as GameObject;
        currentDamagePopup.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
        currentDamagePopup.GetComponent<TMPro.TextMeshProUGUI>().text = (Convert.ToInt32(damage)).ToString();
        if(isPlayer) currentDamagePopup.GetComponent<TMPro.TextMeshProUGUI>().faceColor = Color.red;
    }

    public void HitShake() { shakeFor = maxShakeFor * 0.1f; shakeBy = startShakeBy * 0.2f; }

    public void ChargedShake() { shakeFor = maxShakeFor; shakeBy = startShakeBy; }

    public void ResetShake() { shakeFor = startShakeFor; shakeBy = startShakeBy; }

    public void RumbleController(float rumbleFor) { StartCoroutine(RumbleCounter(rumbleFor)); }
    private IEnumerator RumbleCounter(float rumbleFor)
    {
        Gamepad.current.SetMotorSpeeds(0.123f, 0.234f);
        yield return new WaitForSeconds(rumbleFor);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
