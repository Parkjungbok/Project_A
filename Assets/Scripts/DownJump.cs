using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownJump : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] PlatformEffector2D effector2D;
    public bool player;

    private void Start()
    {
        player = false;
    }


    private void Update()
    {
        
    }


    private void OnTriggerEnter2D( Collider2D collision )
    {
        player = true;
    }
    private void OnTriggerExit2D( Collider2D collision )
    {
        player = false;
    }
}
