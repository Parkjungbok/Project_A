using System.Collections;
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
    private GameObject collisioncheak;
    private Coroutine gateCoroutine;

    private void Update()
    {
        UsingtheGate();
    }


    private IEnumerator MoveCharacter( Vector3 destination )
    {
        FindObjectOfType<PlayerController>().LockCharacterTransform();
        yield return new WaitForSeconds(1f);
        collisioncheak.transform.position = destination;
        FindObjectOfType<PlayerController>().usingTheGate = false;
        FindObjectOfType<PlayerController>().UnlockCharacterTransform();
        gateCoroutine = null;
    }

    public void UsingtheGate()
    {
        if ( cheak == true && FindObjectOfType<PlayerController>().usingTheGate == true )
        {
            if ( nextPositionType == NextPositionType.SomePosiotion )
            {
                if ( gateCoroutine == null )
                {
                    gateCoroutine = StartCoroutine(MoveCharacter(destinationPoint.position));
                }
            }
            else
            {
            }
        }

    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            ani.SetBool("Cheak", true);
            cheak = true;
            collisioncheak = collision.gameObject;
        }
    }


    private void OnTriggerExit2D( Collider2D collision )
    {
        if ( layer.Contain(collision.gameObject.layer) )
        {
            ani.SetBool("Cheak", false);
            cheak = false;
            collisioncheak = collision.gameObject;
        }
    }
}


/*  //추후 사망시 시작지점 위치때 사용
if ( nextPositionType == NextPositionType.InitPosition )
{
   collisioncheak.transform.position = Vector3.zero;
   FindObjectOfType<PlayerController>().usingTheGate = false;
}
*/

