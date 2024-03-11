using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    [SerializeField] LayerMask monsterCheckLayer;
    [SerializeField] int damage;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] ParticleSystem hitEffect;

    private bool monsterCheck;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( ( monsterCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            monsterCheck = true;
            Debug.Log("맞음");
            IDamagable damagable = collision.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);


            Vector2 velocity = rigid.velocity;
            velocity.y = 15;
            rigid.velocity = velocity;
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( ( monsterCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            Debug.Log("안맞음");
            monsterCheck = false;
        }
    }
}

