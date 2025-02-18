using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePos;
    [SerializeField] private float shootDelay = 1.5f;
    [SerializeField] private float fireForce = 10f;

    private float rotateOffset = 180f;
    private Transform player;
    private Enemy enemy;
    private Animator animator;
    private bool isShooting = false;

    void Start()
    {
        //GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        //if (playerObject != null)
        //{
        //    player = playerObject.transform;
        //}

        //enemy = GetComponentInParent<Enemy>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        enemy = GetComponentInParent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            RotateWeapon();
            if (!isShooting)
            {
                StartCoroutine(ShootArrow());
            }
        }
    }

    void RotateWeapon()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float adjustedOffset = (enemy != null && enemy.transform.localScale.x < 0) ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        transform.localScale = new Vector3(enemy.transform.localScale.x < 0 ? -1 : 1, 1, 1);
    }

    IEnumerator ShootArrow()
    {
        isShooting = true;

        // Play the bow drawing animation
        animator.SetTrigger("DrawBow");

        // Wait for the animation to finish before shooting
        yield return new WaitForSeconds(shootDelay);

        // Instantiate and fire the arrow
        GameObject arrow = Instantiate(arrowPrefab, firePos.position, firePos.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePos.right * fireForce;
        }

        isShooting = false;
    }
}
