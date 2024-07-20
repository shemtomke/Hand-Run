using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        Move();
    }
    void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
