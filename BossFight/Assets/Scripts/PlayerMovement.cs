using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float startSpeed = 0.5f, speed;
    private Rigidbody2D rb;
    private Vector2 input, lastMoveDirection, moves;
    Animator anim;

    /* dashing */
    private bool canDash = true, isDashing;
    public float dashPower, dashTime, dashCooldown; // set in engine inspector

    /* attacking */
    private bool canAtk = true;
    public float atkTime, atkCooldown, atkDamage, atkPowerMax; // set in engine inspector
    private float startAtkPower = 1f, atkPower, atkTimerMultiplier = 4f;




    void Start()
    {
        speed = startSpeed;
        atkPower = startAtkPower;
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
    }

    private void FixedUpdate()
    {
        rb.velocity = input * speed;
    }




    void ManageInputs()
    {
        if (isDashing)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moves = new Vector2(moveX, moveY);

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0)) lastMoveDirection = input;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("Dash") && canDash)
        {
            StartCoroutine(Dash(dashPower));
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
        if (Input.GetButtonUp("Attack") && canAtk) //release attack input
        {
            speed = startSpeed; //can move again once releasing attack
            anim.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        if (Input.GetButton("Attack") && canAtk) //hold attack input
        {
            anim.SetTrigger("ChargingAttack"); //should ideally be a boolean to be more efficient
            speed = 0f; //cant move while charging attack
            if (atkPower <= atkPowerMax)
            {
                atkPower += Time.deltaTime * atkTimerMultiplier; //increases atkPower by 2 every second
            }
            else atkPower = atkPowerMax;
            Debug.Log(atkPower);
        }
    }




    private IEnumerator Dash(float power)
    {
        canDash = false;
        isDashing = true;

        speed = power; // during dash

        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        speed = startSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Attack()
    {
        canAtk = false;

        StartCoroutine(Dash(atkPower)); // during attack

        yield return new WaitForSeconds(atkTime);
        anim.SetTrigger("HasAttacked");

        yield return new WaitForSeconds(atkCooldown);
        atkPower = startAtkPower;
        canAtk = true;
    }
}
