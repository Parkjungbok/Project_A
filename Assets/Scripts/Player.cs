using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IDamagable
{
    [SerializeField] int hp;
    [SerializeField] CapsuleCollider2D collider;
    [SerializeField] Animator animator;

    private void Die()
    {
        Destroy(gameObject);
    }   

    private bool canMove = true; // 추가: 플레이어가 움직일 수 있는지 여부를 나타내는 변수

    public void TakeDamage( int damage )
    {
        if ( canMove )
        {
            hp -= damage;
            animator.SetTrigger("IsDamage");

            // 플레이어의 현재 방향을 확인
            int direction = ( transform.localScale.x > 0 ) ? 1 : -1;

            // 키 입력 불가능 상태로 변경
            canMove = false;

            // 2초 후에 다시 키 입력 가능 상태로 변경
            StartCoroutine(EnableMovementAfterDelay(2f));

            // 부딛친 반대 방향으로 x=2 y=2만큼 날아가게 함
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-direction * 2f, 2f);

            if ( hp <= 0 )
            {
                Die();
            }
        }
    }

    private IEnumerator EnableMovementAfterDelay( float delay )
    {
        yield return new WaitForSeconds(delay);
        canMove = true; // 키 입력 가능 상태로 변경
    }

}
