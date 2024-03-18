using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Item : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] AudioClip get;
    
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            MoneyManager.instance.AddMoney(100);
            Manager.Sound.PlaySFX(get);
            Destroy(gameObject);
        }
    }
}
