using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

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

    [Header("Climbing")]
    [SerializeField] LayerMask climbingCheckLayer;
    [SerializeField] float climbingSpeed;

    private bool isClimbing;

    [Header("Jump")]
    [SerializeField] float jumpSpeed;
    [SerializeField] LayerMask groundCheckLayer;
    [SerializeField] ParticleSystem druk;

    private bool isGrounded;

    [Header("Lie")]
    [SerializeField] GameObject childcollider;    
       

    [Header("Weapon")]
    [SerializeField] SpriteRenderer parentSpriteRenderer;
    [SerializeField] SpriteRenderer childSpriteRenderer;
    [SerializeField] Transform weaponRotation;

    private Vector2 moveDir;

    private void FixedUpdate()
    {
        Move();
        InheritFlip();
        Climbing();
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

        animator.SetFloat("XSpeed", rigid.velocity.x);
        animator.SetFloat("YSpeed", rigid.velocity.y);
    }

    private void Climbing()
    {
        
    }

    private void Ceiling()
    {

    }
    private void Lie()
    {
        
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
        rigid.bodyType = RigidbodyType2D.Dynamic;

        moveDir = value.Get<Vector2>();
        if ( moveDir.x < 0 )
        {
            render.flipX = true;
            animator.SetBool("Run", true);
            animator.SetBool("Above", false);
        }
        else if ( moveDir.x > 0 )
        {
            render.flipX = false;
            animator.SetBool("Run", true);
            animator.SetBool("Above", false);
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
            rigid.bodyType = RigidbodyType2D.Dynamic;
            Jump();
        }
    }

    private void OnLie( InputValue value )
    {
        if ( value.isPressed )
        {
            collider.offset = new Vector2(collider.offset.x, -0.45f);
            collider.size = new Vector2(0.8f, 0.8f);
            animator.SetBool("Lie", true);
            //belowCamera.Priority = 11;
        }
        else
        {
            collider.offset = new Vector2(collider.offset.x, -0.15f);
            collider.size = new Vector2(1f, 1.6f);
            animator.SetBool("Lie", false);
            //belowCamera.Priority = 9;
        }
    }

    private void OnAbove( InputValue value )
    {
        if ( value.isPressed )
        {
            animator.SetBool("Above", true);
            //aboveCamera.Priority = 11;
        }
        else
        {
            animator.SetBool("Above", false);
            //aboveCamera.Priority = 9;
        }
    }

    private void OnAttack( InputValue value )
    {
        if ( value.isPressed )
        {
            animator.SetTrigger("Attack");
        }
    }
    /*
    private void OnClimbingUp( InputValue value )
    {
        if ( isClimbing && value.isPressed )
        {
            rigid.bodyType = RigidbodyType2D.Kinematic;
            Vector2 velocity = rigid.velocity;
            velocity.y = climbingSpeed;
            rigid.velocity = velocity;
            animator.SetBool("ClimbingUp", true);
        }
        else
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = 0;
            velocity.x = 0;
            rigid.velocity = velocity;
            animator.SetBool("ClimbingUp", false);
        }
    }
    private void OnClimbingDown( InputValue value )
    {
        if ( isClimbing && value.isPressed )
        {
            rigid.bodyType = RigidbodyType2D.Kinematic;
            Vector2 velocity = rigid.velocity;
            velocity.y = -climbingSpeed;
            rigid.velocity = velocity;
            animator.SetBool("ClimbingDown", true);
        }
        else
        {
            Vector2 velocity = rigid.velocity;
            velocity.y = 0;
            velocity.x = 0;
            rigid.velocity = velocity;
            animator.SetBool("ClimbingDown", false);
        }
    }
    *///사다리 테스트
    private void InheritFlip()
    {
        weaponRotation.localScale = new Vector3(parentSpriteRenderer.flipX ? -1f : 1f, weaponRotation.localScale.y, weaponRotation.localScale.z);
    }

    private int groundCount;
    private int climbingCount;    

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( groundCheckLayer.Contain(collision.gameObject.layer) )
        {
            isGrounded = true;
            groundCount++;
            isGrounded = groundCount > 0;
            animator.SetBool("IsGround", isGrounded);
            if ( druk != null )
            {
                druk.Play();
            }
        }
        if ( climbingCheckLayer.Contain(collision.gameObject.layer) )
        {
            isClimbing = true;
            climbingCount++;
            isClimbing = climbingCount > 0;
            animator.SetBool("IsClimbing", isClimbing);
            animator.SetBool("Above", false);
        }        
    }
    
    // 캐릭터 레이어마스크 그라운드체커
    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( groundCheckLayer.Contain(collision.gameObject.layer) )
        {
            isGrounded = false;
            groundCount--;
            isGrounded = groundCount > 0;
            animator.SetBool("IsGround", isGrounded);
        }

        if ( climbingCheckLayer.Contain(collision.gameObject.layer) )
        {
            isClimbing = false;
            climbingCount--;
            isClimbing = climbingCount > 0;
            animator.SetBool("IsClimbing", isClimbing);
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }        
    }
    
}