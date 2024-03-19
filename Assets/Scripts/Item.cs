using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] AudioClip get;
    [SerializeField] GameObject acquiredEffect;


    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            GameObject deathEffect = Instantiate(acquiredEffect, transform.position, Quaternion.identity);
            Manager.Sound.PlaySFX(get);
            Money.MoneyAmount += 100; //Á¡¼ö
            Destroy(gameObject);
        }
    }
}
