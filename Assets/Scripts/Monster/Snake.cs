using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Snake : MonoBehaviour, IDamagable
{
    private enum State
    {
        Move, Trace, Return, Attack
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;
    [SerializeField] float attackRange;
    [SerializeField] int hp;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] CapsuleCollider2D collider2d;
    [SerializeField] Animator animator;
    [SerializeField] GameObject DieEffect;

    private State curState;
    private Transform playerTransform;
    private Vector3 startPos;

      

    private void Start()
    {        
        curState = State.Move;
        playerTransform = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rigid.velocity = new Vector2(-1, rigid.velocity.y);
        /*
        switch ( curState )
        {
            case State.Move:
                MoveUpdate();
                break;
            case State.Trace:
                TraceUpdate();
                break;
            case State.Return:
                ReturnUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;            
        }
        */
    }    
    
    private void MoveUpdate()
    {
        // �ݺ� �̵�����        
        rigid.velocity = new Vector2(-1, rigid.velocity.y);
        
        if ( Vector2.Distance(playerTransform.position, transform.position) < findRange )
        {
            curState = State.Trace; // �i�ư���
        }
    }
    private void TraceUpdate()
    {
        Vector3 dir = ( playerTransform.position - transform.position ).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        // �����Ÿ� �־����� ���ư�
        if ( Vector2.Distance(playerTransform.position, transform.position) > findRange )
        {
            curState = State.Return;
        }
        // �����Ÿ� �ȿ� ���ͼ� ������
        if ( Vector2.Distance(playerTransform.position, transform.position) < attackRange )
        {
            curState = State.Attack;
        }        
    }
    private void ReturnUpdate()
    {
        Vector2 dir = ( ( Vector3 ) startPos - transform.position ).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        if ( Vector2.Distance(playerTransform.position, transform.position) < findRange )
        {
            curState = State.Trace; // �i�ư���
        }

        if ( Vector2.Distance(transform.position, startPos) < 0.01f )
        {
            curState = State.Move;
        }
    }
    private void AttackUpdate()
    {
        Vector3 dir = ( playerTransform.position - transform.position ).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        if ( Vector2.Distance(playerTransform.position, transform.position) > 2f )
        {            
            animator.SetTrigger("Attack");
        }

        if ( Vector2.Distance(playerTransform.position, transform.position) > findRange )
        {
            curState = State.Return;
        }
    }    

    private void Die()
    {
        Debug.Log("����Ʈ �ߵ���");
        GameObject deathEffect = Instantiate(DieEffect, transform.position, Quaternion.identity);
        Debug.Log("����Ʈ �ߵ��Ϸ�");
        Destroy(gameObject);
    }
    public void TakeDamage( int damage )
    {
        hp -= damage;
        if ( hp <= 0 )
        {
            Die();
        }
    }
}
