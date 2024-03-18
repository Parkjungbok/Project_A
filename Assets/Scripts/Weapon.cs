using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask monsterCheckLayer;
    [SerializeField] int damage;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( ( monsterCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {            
            IDamagable damagable = collision.GetComponent<IDamagable>();
            damagable?.TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( ( monsterCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            
        }
    }
}
