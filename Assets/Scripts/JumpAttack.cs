using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour
{
    [SerializeField] LayerMask monsterCheckLayer;
    [SerializeField] int damage;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] AudioClip jumpAttack;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( ( monsterCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            
            Debug.Log("몬스터를 밟음");
            IDamagable damagable = collision.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);
            Manager.Sound.PlaySFX(jumpAttack);

            Vector2 velocity = rigid.velocity;
            velocity.y = 15;
            rigid.velocity = velocity;
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( ( monsterCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            
        }
    }
}

