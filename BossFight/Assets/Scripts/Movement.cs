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

    private IEnumerator Dash()
    {
        /////////////////start dash
        canDash = false;
        isDashing = true;

        speed = dashPower;

        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        speed = startSpeed;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        ////////////////////////end dash
    }
}
