using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] LayerMask PlayerCheckLayer;
    [SerializeField] int damage;    
    [SerializeField] Rigidbody2D rigid;    

    private bool playerCheck;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( ( PlayerCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            playerCheck = true;
            Debug.Log("�÷��̾� ����");
            IDamagable damagable = collision.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( ( PlayerCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            Debug.Log("�÷��̾������");
            playerCheck = false;
        }
    }
}
