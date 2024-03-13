using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private enum State
    {
        Move, Trace, Return, Attack, Died
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;
    [SerializeField] float hp;

    private State curState;
    private Transform playerTransform;
    private Vector3 startPos;

    private void Start()
    {
        curState = State.Move;
        playerTransform = GameObject.FindWithTag("Player").transform;
        startPos = transform.position;
    }

    private void Update()
    {
        switch(curState)
        {
            case State.Move:
                MoveUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Died:
                DiedUpdate();
                break;
        }
    }

    private void MoveUpdate()
    {
        if( Vector2.Distance(playerTransform.position, transform.position)<findRange)
        {
            curState = State.Attack;
        }
    }
    private void AttackUpdate()
    {

    }
    private void DiedUpdate()
    {

    }

    
}
