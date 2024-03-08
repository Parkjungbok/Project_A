using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask monsterCheckLayer;
    [SerializeField] int damage;
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
