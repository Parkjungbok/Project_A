using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;
    [SerializeField] AudioClip die;
    [SerializeField] GameObject bone;

    private void Die()
    {
        GameObject deathEffect = Instantiate(bone, transform.position, Quaternion.identity);
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
