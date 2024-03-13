using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMon : MonoBehaviour
{
    Rigidbody2D rigid;
    public float moveSpeed;
    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        Invoke("Think", 3);
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y * moveSpeed * Time.deltaTime);
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 3);
    }
}
