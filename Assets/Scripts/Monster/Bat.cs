using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{    
    [SerializeField] float moveSpeed;
    [SerializeField] float findRange;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 playerPos = playerTransform.position;

        Vector2 dir = (playerPos - transform.position).normalized;

        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
