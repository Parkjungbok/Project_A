using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRight : MonoBehaviour
{  
    [SerializeField] LayerMask damageCheckLayer;    
    [SerializeField] Rigidbody2D rigid;

    private bool rightCheck;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( ( damageCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            rightCheck = true;
            Debug.Log("몬스터를 밟음");

            Vector2 velocity = rigid.velocity;
            velocity.y = 15;
            velocity.x = -15;
            rigid.velocity = velocity;
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( ( damageCheckLayer.value & 1 << collision.gameObject.layer ) != 0 )
        {
            Debug.Log("몬스터를 밟지 않음");
            rightCheck = false;
        }
    }
}
