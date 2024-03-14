using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMon : MonoBehaviour
{
    private enum State
    {
        Move, Trace, Return, Attack
    }
    [SerializeField] float attackMoveSpeed;
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
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 1);
    }
    private void Start()
    {
        curState = State.Move;
        playerTransform = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
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
        
    }
    private void MoveUpdate()
    {        
        if ( Vector2.Distance(playerTransform.position, transform.position) < findRange )
        {
            curState = State.Trace; // 쫒아가기
        }
        else
        {
            Move();
        }        
    }
    private void TraceUpdate()
    {
        Vector3 dir = ( playerTransform.position - transform.position ).normalized;
        transform.Translate(dir * attackMoveSpeed * Time.deltaTime);

        // 일정거리 멀어져서 돌아감
        if ( Vector2.Distance(playerTransform.position, transform.position) > findRange )
        {
            curState = State.Return;
        }
        // 일정거리 안에 들어와서 공격함
        if ( Vector2.Distance(playerTransform.position, transform.position) < attackRange )
        {
            curState = State.Attack;
        }
    }
    private void ReturnUpdate()
    {
        Vector2 dir = ( ( Vector3 ) startPos - transform.position ).normalized;
        transform.Translate(dir * attackMoveSpeed * Time.deltaTime, Space.World);

        if ( Vector2.Distance(playerTransform.position, transform.position) < findRange )
        {
            curState = State.Trace; // 쫒아가기
        }
        else
        {
            curState = State.Move;
        }
        // 제자리 컴백
        /*
        if ( Vector2.Distance(transform.position, startPos) < 0.1f )
        {
            curState = State.Move;
        }
        */
    }
    private void AttackUpdate()
    {
        Vector3 dir = ( playerTransform.position - transform.position ).normalized;
        transform.Translate(dir * attackMoveSpeed * Time.deltaTime);

        if ( Vector2.Distance(playerTransform.position, transform.position) > 2f )
        {
            //animator.SetTrigger("Attack");
        }

        if ( Vector2.Distance(playerTransform.position, transform.position) > findRange )
        {
            curState = State.Return;
        }
    }

    public void Move()
    {
        rigid.velocity = new Vector2(1.5f, rigid.velocity.y);
    }

    public void Think()
    {
        nextMove = Random.Range(-2, 3);
        Invoke("Think", 1);
    }    
}
