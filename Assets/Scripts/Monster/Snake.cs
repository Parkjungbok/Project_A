using UnityEngine;

public class Snake : MonoBehaviour, IDamagable
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
    [SerializeField] SpriteRenderer render;
    [SerializeField] GameObject DieEffect;

    [Header("Sound")]
    [SerializeField] AudioClip attack;
    [SerializeField] AudioClip die;

    private State curState;
    private Transform playerTransform;
    private Vector3 startPos;
    private int nextMove;
    private int randThink;

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

        if ( playerTransform.position.x - transform.position.x < 0 )
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }

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

        if ( nextMove < 0 )
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }

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

        if ( playerTransform.position.x - transform.position.x < 0 )
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }

        if ( Vector2.Distance(playerTransform.position, transform.position) > 2f )
        {
            animator.SetTrigger("Attack");
            Manager.Sound.PlaySFX(attack);
        }

        if ( Vector2.Distance(playerTransform.position, transform.position) > findRange )
        {
            curState = State.Return;
        }
    }

    

    public void Move()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        if ( nextMove < 0 )
        {
            render.flipX = true;
        }
        else
        {
            render.flipX = false;
        }
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f , rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 1);
        }

    }

    public void Think()
    {
        nextMove = Random.Range(-2, 3);
        randThink = Random.Range(0, 3);
        Invoke("Think", randThink);
    }

    private void Die()
    {
        
        GameObject deathEffect = Instantiate(DieEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
    public void TakeDamage( int damage )
    {
        hp -= damage;
        if ( hp <= 0 )
        {
            Manager.Sound.PlaySFX(die);
            Die();
        }
    }
}
