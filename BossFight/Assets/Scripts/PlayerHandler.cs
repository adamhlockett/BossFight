using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 input, lastMoveDirection, moves;
    Animator anim;
    [SerializeField] float startSpeed; // set in engine inspector
    private float speed;

    /* dashing */
    private bool canDash = true, isDashing;
    [SerializeField] float dashDistance; // set in engine inspector
    private float dashTime = 0.2f, dashCooldown = 0.5f;

    /* attacking */
    private bool canAtk = true;
    [SerializeField] float atkDamage, atkDistanceMax; // set in engine inspector
    private float atkTime = 0.2f, atkCooldown = 0.2f, startAtkDistance = 1f, atkDistance, atkTimerMultiplier;

    /* visual feedback */
    [SerializeField] Camera cam;
    private float shakeFor = 0f, shakeBy = 0.05f, decrementBy = 4f, maxShakeFor = 0.5f;



    void Start()
    {
        speed = startSpeed;
        atkDistance = startAtkDistance;
        atkTimerMultiplier = atkDistanceMax * 0.7f;
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
                shakeFor = maxShakeFor;                                                                                   //hold attack
            }                                                                                                       //hold attack
        }                                                                                                           //hold attack
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

    private IEnumerator Attack(float damage)
    {
        canAtk = false;

        StartCoroutine(Dash(atkDistance)); // during attack
        Debug.Log(damage);

        yield return new WaitForSeconds(atkTime);
        anim.SetTrigger("HasAttacked");

        yield return new WaitForSeconds(atkCooldown);
        atkDistance = startAtkDistance;
        canAtk = true;
    }

    private void ManageVisualFeedback()
    {
        if (shakeFor > 0)
        {
            cam.transform.localPosition = new Vector3 ( Random.insideUnitSphere.x * shakeBy, Random.insideUnitSphere.y * shakeBy, cam.transform.localPosition.z );
            shakeFor -= Time.deltaTime * decrementBy;
        }
        else shakeFor = 0f;
    }
}
