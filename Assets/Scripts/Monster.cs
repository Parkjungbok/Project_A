using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamagable
{
    [SerializeField] LayerMask PlayerCheckLayer;
    [SerializeField] int damage;    
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] int hp;

    private bool playerCheck;    

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage( int damage )
    {
        hp -= damage;
        if ( hp <= 0 )
        {
            Die();
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( ( PlayerCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            playerCheck = true;
            Debug.Log("플레이어맞음");
            IDamagable damagable = collision.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( ( PlayerCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            Debug.Log("플레이어안맞음");
            playerCheck = false;
        }
    }
}
