using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour , IDamagable
{
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] CapsuleCollider2D collider2d;
    [SerializeField] int hp;
    [SerializeField] Animator animator;

    private void Update()
    {
        /*
        if (!isDie)
        {
            Move();
            if ( !IsgroundExist() )
                Turn();
        }
        */
    }

    private void Move()
    {
        //animator.SetBool("Move", true);
    }



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
}
