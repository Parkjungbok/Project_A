using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using static Gate;

public class PlayerController : MonoBehaviour, IDamagable
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
    private PlayerCeiling childScriptReference;

    [Header("Weapon")]
    [SerializeField] SpriteRenderer parentSpriteRenderer;
    [SerializeField] SpriteRenderer childSpriteRenderer;
    [SerializeField] Transform weaponRotation;

    [Header("Gate")]
    [SerializeField] LayerMask gateCheak;
    private bool isGated;
    public bool usingTheGate;

    [Header("Damage")]

    [SerializeField] float hp;
    [SerializeField] LayerMask damageLayer;

    [Header("Sound")]

    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip jumpattack;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip hitbrid;

    private bool isDamaged;
    private bool isInputBlocked;

    private Vector2 moveDir;

    private void Start()
    {
        childScriptReference = GetComponentInChildren<PlayerCeiling>();
    }

    private void FixedUpdate()
    {
        if ( !isInputBlocked )
        {
            Move();
            InheritFlip();
        }

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

    protected void StandDown()
    {
        collider.offset = new Vector2(collider.offset.x, -0.545f);
        collider.size = new Vector2(0.8f, 0.8f);
        animator.SetBool("Lie", true);
    }
    protected void StandUp()
    {
        collider.offset = new Vector2(collider.offset.x, -0.15f);
        collider.size = new Vector2(1f, 1.6f);
        animator.SetBool("Lie", false);
    }

    private void Lie()
    {

    }

    private void Attack()
    {
        if ( animator.GetBool("Above") != true )
        {
            Manager.Sound.PlaySFX(attack);
            animator.SetTrigger("Attack");
        }
    }

    private void Jump()
    {
        if ( isGrounded )
        {
            Manager.Sound.PlaySFX(jump);
            Vector2 velocity = rigid.velocity;
            velocity.y = jumpSpeed;
            rigid.velocity = velocity;
        }
    }

    public void InGate()
    {
        usingTheGate = true;
        animator.SetTrigger("InGate");
    }


    // 이동 입력
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
    // 점프 입력
    private void OnJump( InputValue value )
    {
        if ( value.isPressed )
        {
            if ( !isInputBlocked )
            {
                Jump();
            }
        }
    }
    // 엎드리기 입력
    private void OnLie( InputValue value )
    {
        if ( value.isPressed || childScriptReference.isCeiling )
        {
            collider.offset = new Vector2(collider.offset.x, -0.545f);
            collider.size = new Vector2(0.8f, 0.8f);
            animator.SetBool("Lie", true);
            belowCamera.Priority = 11;
        }
        else
        {
            collider.offset = new Vector2(collider.offset.x, -0.15f);
            collider.size = new Vector2(1f, 1.6f);
            animator.SetBool("Lie", false);
            belowCamera.Priority = 9;
        }
    }
    // 위에 보기 입력
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
    // 공격 입력
    private void OnAttack( InputValue value )
    {
        Attack();
    }

    public void OnInStage( InputValue value )
    {
        
        if ( isGated )
        {            
            InGate();
        }
        else
        {
        }
    }


    //사다리 테스트---------------------------------------------------------------------
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
    */


    // 무기방향 반전
    private void InheritFlip()
    {
        weaponRotation.localScale = new Vector3(parentSpriteRenderer.flipX ? -1f : 1f, weaponRotation.localScale.y, weaponRotation.localScale.z);
    }

    private int groundCount;
    private int climbingCount;
    private int damageCount;
    private int gateCount;

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
            animator.SetBool("IsGround", true);
        }
        if ( damageLayer.Contain(collision.gameObject.layer) )
        {
            isDamaged = true;
            damageCount++;
            isDamaged = damageCount > 0;
            animator.SetTrigger("IsDamage");
        }
        if ( gateCheak.Contain(collision.gameObject.layer) )
        {
            isGated = true;
            gateCount++;
            isGated = gateCount > 0;
            animator.SetBool("IsGate", true);
        }

    }

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
            animator.SetBool("IsGround", false);
        }
        if ( damageLayer.Contain(collision.gameObject.layer) )
        {
            isDamaged = false;
            damageCount--;
            isDamaged = damageCount > 0;
        }
        if ( gateCheak.Contain(collision.gameObject.layer) )
        {
            isGated = true;
            gateCount--;
            isGated = gateCount > 0;
            animator.SetBool("IsGate", false);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage( int damage )
    {
        if ( !isDamaged )
        {
            isDamaged = true;
            isInputBlocked = true;
            StartCoroutine(DisableInputForSeconds(2f)); // Disable input for 2 seconds
            Manager.Sound.PlaySFX(hit);
            Manager.Sound.PlaySFX(hitbrid);


            hp -= damage;

            if ( hp <= 0 )
            {
                Die();
            }
        }
    }


    private IEnumerator DisableInputForSeconds( float seconds )
    {
        yield return new WaitForSeconds(seconds);
        isInputBlocked = false;
        isDamaged = false;
    }
}