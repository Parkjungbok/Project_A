using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public enum NextPositionType
    {
        InitPosition, SomePosiotion
    };
    public NextPositionType nextPositionType;
    public Transform DestinationPoint;

    [SerializeField] GameObject player;
    [SerializeField] BoxCollider2D check;
    [SerializeField] LayerMask layer;
    [SerializeField] Animator ani;

    private bool cheak;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            ani.SetBool("Cheak", true);
            cheak = true;
            // �÷��̾ ��ư�� �ߵ��ߴ��� Ȯ��


            if ( player.GetComponent<PlayerController>())
            {
                if ( nextPositionType == NextPositionType.InitPosition )
                {
                    collision.transform.position = Vector3.zero;
                }
                else if ( nextPositionType == NextPositionType.SomePosiotion )
                {
                    collision.transform.position = DestinationPoint.position;
                }
                else
                {                    
                }
            }
        }
    }


    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            ani.SetBool("Cheak", false);
            cheak = false;
        }
    }
}
