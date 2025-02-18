using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float rotateOffSet = 180f;
    private PlayerMovement player;
    private Animator animator;

    public Transform atkPoint; // Attack point reference
    public int damage = 20;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        RotateWeapon();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void RotateWeapon()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Vector3 orient = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(orient.y, orient.x) * Mathf.Rad2Deg;
        float adjustedOffset = player.otherDirection == -1 ? 0f : 180f;

        bool isFacingLeft = player.otherDirection == -1;

        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffSet);
        transform.localScale = new Vector3(isFacingLeft ? -1 : 1, 1, 1);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void DealDamage() // Called by Animation Event
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atkPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (atkPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPoint.position, attackRange);
    }
}
