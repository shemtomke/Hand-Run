using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float speed;
    public Vector3 startPos;

    private void Start()
    {
        transform.position = startPos;
    }
    private void Update()
    {
        Move();

        if(transform.position.x > 12)
        {
            Destroy(this.gameObject);
        }
    }
    void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
