using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float atkRange = 2;
    public float atkCD = 2;
    public float playerDetectRange = 5;
    public Transform dectionPoint;
    public LayerMask playerLayer;


    private float atkCDTimer;
    private int otherDirection = 1;
    private EnemyState state;
    private Rigidbody2D rb;
    private Transform player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (atkCDTimer > 0)
    //    {
    //        atkCDTimer -= Time.deltaTime;
    //    }

    //    if (state == EnemyState.Chasing)
    //    {
    //        if (player.position.x > transform.position.x && otherDirection == -1 || player.position.x < transform.position.x && otherDirection == 1)
    //        {
    //            Flip();
    //        }

    //        Vector2 direction = (player.position - transform.position).normalized;
    //        rb.velocity = direction * speed;

    //    } 
    //    else if (state == EnemyState.Attacking)
    //    {
    //        rb.velocity = Vector2.zero;
    //    }
    //}

    void Update()
    {
        CheckForPlayer();

        if (atkCDTimer > 0)
        {
            atkCDTimer -= Time.deltaTime;
        }

        if (state == EnemyState.Chasing)
        {
            Chase();
        }
        else if (state == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(dectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= atkRange && atkCDTimer <= 0)
            {
                atkCDTimer = atkCD;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > atkRange)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        rb.velocity = Vector2.zero;
    //        ChangeState(EnemyState.Idle);
    //    }
    //}

    void Flip()
    {
        otherDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void ChangeState(EnemyState newState)
    {
        /// Exit current animation
        animator.SetBool("isIdle", false);
        animator.SetBool("isChasing", false);
        animator.SetBool("isAttacking", false); // Ensure it resets before setting

        state = newState;

        // Update new animation
        if (state == EnemyState.Idle)
            animator.SetBool("isIdle", true);
        else if (state == EnemyState.Chasing)
            animator.SetBool("isChasing", true);
        else if (state == EnemyState.Attacking)
        {
            animator.SetBool("isAttacking", true);
            rb.velocity = Vector2.zero; // Stop movement during attack
        }
    }

    void Chase()
    {
        if (player.position.x > transform.position.x && otherDirection == -1 || player.position.x < transform.position.x && otherDirection == 1)
        {
            Flip();
        } 

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
}
