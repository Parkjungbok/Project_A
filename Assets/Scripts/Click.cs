using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Click : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform endPoint;

    private void MouseClick(Vector2 point )
    {
        agent.destination = point;
    }

    private void OnRightClick(InputValue value)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        
    }
}
