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

        // Check if the collider is disabled && 
    }
    void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    // flame goes from the arms to the character,if character misses the flame it goes to pick-up
    public void DisableCollider()
    {
        this.GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
