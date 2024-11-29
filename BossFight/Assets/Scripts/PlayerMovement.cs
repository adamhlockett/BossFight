using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float startSpeed = 0.5f, speed;
    private Rigidbody2D rb;
    private Vector2 input, lastMoveDirection;
    Animator anim;
    Vector2 moves;

    /* dashing */
    private bool canDash = true, isDashing;
    public float dashPower = 24f, dashTime = 0.2f, dashCooldown = 1f;

    /* attacking */
    private bool canAtk = true;
    public float atkPower = 5f, atkTime = 0.5f, atkCooldown = 0.5f, atkDamage = 20f;

    void Start()
    {
        speed = startSpeed;
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
            StartCoroutine(Dash());
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
            //anim.ResetTrigger("ChargingAttack");
            speed = startSpeed;
            anim.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
        if (Input.GetButton("Attack") && canAtk) //hold attack input
        {
            Debug.Log("holding attack");
            anim.SetTrigger("ChargingAttack");
            speed = 0f;
        }
    }

    private IEnumerator Dash()
    {
        /////////////////start dash
        canDash = false;
        isDashing = true;

        speed = dashPower; // during dash

        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        speed = startSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        ////////////////////////end dash
    }

    private IEnumerator Attack()
    {
        //continuously trigger animation while holding X (X input)
        //dash player forward while attacking (release of X)
        canAtk = false;

        StartCoroutine(Dash()); // during attack

        yield return new WaitForSeconds(atkTime);
        anim.SetTrigger("HasAttacked");
        Debug.Log("has attacked");

        //speed = startSpeed;
        yield return new WaitForSeconds(atkCooldown);
        canAtk = true;
    }
}
