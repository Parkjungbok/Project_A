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

    private bool canMove = true; // �߰�: �÷��̾ ������ �� �ִ��� ���θ� ��Ÿ���� ����

    public void TakeDamage( int damage )
    {
        if ( canMove )
        {
            hp -= damage;
            animator.SetTrigger("IsDamage");

            // �÷��̾��� ���� ������ Ȯ��
            int direction = ( transform.localScale.x > 0 ) ? 1 : -1;

            // Ű �Է� �Ұ��� ���·� ����
            canMove = false;

            // 2�� �Ŀ� �ٽ� Ű �Է� ���� ���·� ����
            StartCoroutine(EnableMovementAfterDelay(2f));

            // �ε�ģ �ݴ� �������� x=2 y=2��ŭ ���ư��� ��
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
        canMove = true; // Ű �Է� ���� ���·� ����
    }

}
