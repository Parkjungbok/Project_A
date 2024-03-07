using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] SpriteRenderer render;
    [SerializeField] Animator animator;
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera aboveCamera;
    [SerializeField] CinemachineVirtualCamera belowCamera;
    [SerializeField] CapsuleCollider2D collider;

    [Header("Move")]
    [SerializeField] float moveSpeed;
    [SerializeField] float breakPower;
    [SerializeField] float maxXSpeed;
    [SerializeField] float maxYSpeed;

    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] LayerMask groundCheckLayer;
    [SerializeField] LayerMask ceilingCheckLayer;
    [SerializeField] ParticleSystem druk;
    private bool isGrounded;
    private bool isCeiling;

    private Vector2 moveDir;


    private void FixedUpdate()
    {
        Move();       
    }


    private void Move()
    {
        if ( moveDir.x < 0 && rigid.velocity.x > -maxXSpeed )
        {
            rigid.AddForce(Vector2.right * moveSpeed * moveDir.x);
        }
        else if ( moveDir.x > 0 && rigid.velocity.x < maxXSpeed )
        {
            rigid.AddForce(Vector2.right * moveSpeed * moveDir.x);
        }
        else if ( moveDir.x == 0 && rigid.velocity.x > 0.1f )
        {
            rigid.AddForce(Vector2.left * breakPower);
        }
        else if ( moveDir.x == 0 && rigid.velocity.x < -0.1f )
        {
            rigid.AddForce(Vector2.right * breakPower);
        }

        if ( rigid.velocity.y < -maxYSpeed )
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = -maxYSpeed;
            rigid.velocity = velocity;
        }


        animator.SetFloat("YSpeed", rigid.velocity.y);
    }

    private void Jump()
    {
        if ( isGrounded )
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = jumpSpeed;
            rigid.velocity = velocity;
        }
    }

    

    private void OnMove( InputValue value )
    {
        moveDir = value.Get<Vector2>();
        if ( moveDir.x < 0 )
        {
            render.flipX = true;
            animator.SetBool("Run", true);
        }
        else if ( moveDir.x > 0 )
        {
            render.flipX = false;
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void OnJump( InputValue value )
    {
        if ( value.isPressed )
        {
            Debug.Log("มกวม");
            Jump();
        }
    }

    private void OnLie( InputValue value )
    {
        if ( value.isPressed )
        {
            collider.offset = new Vector2(collider.offset.x, -0.43f);
            collider.size = new Vector2(0.8f, 0.8f);
            animator.SetBool("Lie", true);
            if ( !lying )
            {
                lying = true;
                lieStartTime = Time.time;
            }

            if ( Time.time - lieStartTime >= 3.0f )
            {
                belowCamera.Priority = 11;
            }

        }
        else
        {
            collider.offset = new Vector2(collider.offset.x, -0.15f);
            collider.size = new Vector2(1f, 1.6f);
            animator.SetBool("Lie", false);
            belowCamera.Priority = 9;
        }
    }

    private bool lying = false;
    private float lieStartTime;

    


    private void OnAbove( InputValue value )
    {
        if ( value.isPressed )
        {
            animator.SetBool("Above", true);
            aboveCamera.Priority = 11;
        }
        else
        {
            animator.SetBool("Above", false);
            aboveCamera.Priority = 9;
        }
    }

    private void OnAttack( InputValue value )
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
        if ( groundCheckLayer.Contain(collision.gameObject.layer) )
        {
            isGrounded = true;
            animator.SetBool("IsGround", isGrounded);
            if ( druk != null )
            {
                druk.Play();
            }
        }

        if ( ceilingCheckLayer.Contain(collision.gameObject.layer))
        {
            isCeiling = false;
            animator.SetBool("IsCeiling", isCeiling);
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( groundCheckLayer.Contain(collision.gameObject.layer) )
        {
            isGrounded = false;
            animator.SetBool("IsGround", isGrounded);
        }

        if ( ceilingCheckLayer.Contain(collision.gameObject.layer) )
        {
            isCeiling = true;
            animator.SetBool("IsCeiling", isCeiling);
        }
    }
}
