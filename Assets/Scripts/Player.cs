using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    private bool isGrounded;
    bool isDead = false;
    bool isHit = false;

    // Animation States
    const string PLAYER_RUN = "CharacterRun";
    const string PLAYER_JUMP = "CharacterJump";
    const string PLAYER_DEATH = "CharacterDeath";

    Animator animator;
    Rigidbody2D rb;

    string currentState;

    DistanceManager distanceManager;
    private void Start()
    {
        distanceManager = FindObjectOfType<DistanceManager>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Jump();
    }
    private void Update()
    {
        Death();
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
    void Death()
    {
        if (transform.position.x <= -4)
        {
            isDead = true;
            ChangeAnimationState(PLAYER_DEATH);
        }
    }
    // This method checks if the player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isHit && !isDead)
        {
            isGrounded = true;
            ChangeAnimationState(PLAYER_RUN);
        }

        if(collision.gameObject.CompareTag("Flame"))
        {
            // We can disable the collider on hit player
            Destroy(collision.gameObject);
            MovePlayer(-0.5f);
            distanceManager.UpdateDistance();
            StartCoroutine(FlameHit());
        }

        if (collision.gameObject.CompareTag("Arms"))
        {
            isDead = true;
            ChangeAnimationState(PLAYER_DEATH);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // if the flame touches the character distance from pick-up to character increases and distance from arms to pick-up reduces.
    void MovePlayer(float pos)
    {
        transform.position = new Vector3(transform.position.x + pos, transform.position.y, transform.position.z);
    }

    IEnumerator FlameHit()
    {
        isHit = true;

        if(transform.position.x > -4) { ChangeAnimationState(PLAYER_DEATH); }   

        yield return new WaitForSeconds(0.4f);

        isHit = false;
        ChangeAnimationState(PLAYER_RUN);
    }
}
