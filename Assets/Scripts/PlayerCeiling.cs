using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerCeiling : MonoBehaviour
{
    [Header("Lie")]
    [SerializeField] Animator animator;
    [SerializeField] LayerMask ceilingCheckLayer;
    [SerializeField] CapsuleCollider2D parentCollider;

    public bool isCeiling;

    private int ceilingCount;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (ceilingCheckLayer.Contain(collision.gameObject.layer))
        {
            Debug.Log("머리가닿았다");
            isCeiling = true;
            ceilingCount++;
            isCeiling = ceilingCount > 0;
            animator.SetBool("IsCeiling", isCeiling);
            
            
        }
    }
    
    public void OnTriggerExit2D( Collider2D collision )
    {        
        if ( ceilingCheckLayer.Contain(collision.gameObject.layer) )
        {
            Debug.Log("머리가떨어졌다");
            isCeiling = false;
            ceilingCount--;
            isCeiling = ceilingCount > 0;
            animator.SetBool("IsCeiling", isCeiling);         
            
            
        }
    }
}