using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    private bool isGrounded;

    // Animation States
    const string PLAYER_RUN = "CharacterRun";
    const string PLAYER_JUMP = "CharacterJump";
    const string PLAYER_DEATH = "CharacterDeath";

    Animator animator;
    Rigidbody2D rb;

    string currentState;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Jump();
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Apply the jump force
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Change animation state to jump
            ChangeAnimationState(PLAYER_JUMP);
        }  
    }
    // This method checks if the player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            // Change animation state back to idle or run when grounded
            // You might need to add logic to switch between idle and run
            ChangeAnimationState(PLAYER_RUN);
        }

        if(collision.gameObject.CompareTag("Flame"))
        {
            Debug.Log("Hit Player!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
