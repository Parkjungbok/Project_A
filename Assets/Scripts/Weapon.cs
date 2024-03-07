using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] LayerMask monsterCheckLayer;


    private void OnAttack(InputValue value)
    {
        if ( value.isPressed )
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }        
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( monsterCheckLayer.Contain(collision.gameObject.layer) )
        {            
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( monsterCheckLayer.Contain(collision.gameObject.layer) )
        {            
            animator.SetBool("IsGround", false);
        }
    }


}
