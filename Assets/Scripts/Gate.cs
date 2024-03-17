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
    public Transform destinationPoint;

    [SerializeField] GameObject player;
    [SerializeField] BoxCollider2D check;
    [SerializeField] LayerMask layer;
    [SerializeField] Animator ani;

    private bool cheak;    


    public void UsingtheGate( Collider2D collision )
    {
        if( FindObjectOfType<PlayerController>().usingTheGate == true )
        {
            Debug.Log("작동확인1");
        }
        Debug.Log("작동확인2");
        if ( cheak == true && FindObjectOfType<PlayerController>().usingTheGate == true )
        {
            if ( nextPositionType == NextPositionType.InitPosition )
            {
                collision.transform.position = Vector3.zero;
            }
            else if ( nextPositionType == NextPositionType.SomePosiotion )
            {
                collision.transform.position = destinationPoint.position;
            }
            else
            {
                // 다른 경우에 대한 처리
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            ani.SetBool("Cheak", true);
            cheak = true;
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
