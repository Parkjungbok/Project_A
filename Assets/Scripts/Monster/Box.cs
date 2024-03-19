using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour , IDamagable
{
    [SerializeField] int hp;
    [SerializeField] AudioClip die;
    [SerializeField] GameObject dieEffect;
    [SerializeField] GameObject snakePrefab;
    [SerializeField] GameObject gemPrefab;

    private void Die()
    {
        GameObject deathEffect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        float randomValue = Random.value;
        if ( randomValue <= 0.33f )
        {
            Instantiate(snakePrefab, transform.position, Quaternion.identity);
        }
        else if ( randomValue <= 0.66f )
        {
            Instantiate(gemPrefab, transform.position, Quaternion.identity);
        }
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