using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk : MonoBehaviour
{
    public Transform atkPoint;
    public float weaponRange;
    public LayerMask mask;

    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null) // Ensure the player has the component
        {
            player.TakeDamage(damage);
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(atkPoint.position, weaponRange, mask);
        
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
