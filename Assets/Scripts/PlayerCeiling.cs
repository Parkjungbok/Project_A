using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerCeiling : MonoBehaviour
{
    [Header("Lie")]
    [SerializeField] Animator animator;
    [SerializeField] LayerMask ceilingCheckLayer;
    [SerializeField] CapsuleCollider2D parentCollider;

    private bool isCeiling;
    private int ceilingCount;    

    private void OnTriggerEnter2D( Collider2D collision )
    {        
        if ( ceilingCheckLayer.Contain(collision.gameObject.layer) )
        {
            Debug.Log("´ê¾Ò´Ù");            
            isCeiling = true;
            ceilingCount++;
            isCeiling = ceilingCount > 0;
            animator.SetBool("IsCeiling", isCeiling);
            if ( isCeiling && parentCollider != null )
            {                
                parentCollider.offset = new Vector2(parentCollider.offset.x, -0.45f);
                parentCollider.size = new Vector2(0.8f, 0.8f);
                animator.SetBool("Lie", true);
            }
        }
    }
    
    private void OnTriggerExit2D( Collider2D collision )
    {        
        if ( ceilingCheckLayer.Contain(collision.gameObject.layer) )
        {
            Debug.Log("¶³¾îÁ³´Ù");
            isCeiling = false;
            ceilingCount--;
            isCeiling = ceilingCount > 0;
            animator.SetBool("IsCeiling", isCeiling);
            if ( isCeiling && parentCollider != null )
            {
                parentCollider.offset = new Vector2(parentCollider.offset.x, -0.15f);
                parentCollider.size = new Vector2(1f, 1.6f);
                animator.SetBool("Lie", false);
            }
        }
    }
}