using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5f;
    private bool isGrounded;
    bool isDead = false;
    bool isHit = false;
    string currentState;

    // Animation States
    [Header("Animation States")]
    [SerializeField] string PLAYER_RUN = "CharacterRun";
    [SerializeField] string PLAYER_JUMP = "CharacterJump";
    [SerializeField] string PLAYER_DEATH = "CharacterDeath";

    [Header("Sounds")]
    public AudioClip jumpSound;
    public AudioClip runSound;
    public AudioClip onAirSound;
    public AudioClip landSound;

    Animator animator;
    Rigidbody2D rb;
    DistanceManager distanceManager;
    FlameManager flameManager;
    GameManager gameManager;
    SoundManager soundManager;
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        distanceManager = FindObjectOfType<DistanceManager>();
        flameManager = FindObjectOfType<FlameManager>();
        gameManager = FindObjectOfType<GameManager>();

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

        if (isGrounded && !isDead)
        {
            soundManager.PlaySound(soundManager.runningSound, runSound);
        }
        else
        {
            soundManager.PlaySound(soundManager.jumpingSound, jumpSound);
        }
    }
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
    void Jump()
    {
        // player can tap anywhere on screen to jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || IsTouchInputDetected()) && isGrounded)
        {
            // Apply the jump force
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Change animation state to jump
            ChangeAnimationState(PLAYER_JUMP);
        }  
    }
    private bool IsTouchInputDetected()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
    void Death()
    {
        if (gameManager.IsGameOver())
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
            Flame flame = collision.gameObject.GetComponent<Flame>();
            flame.DisableCollider();

            MovePlayer(-0.5f);
            distanceManager.UpdateDistance();
            StartCoroutine(FlameHit());

            if (distanceManager.IsCloseToDoor()) { soundManager.PlaySound(soundManager.closeToLeftArmsSound); }
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
    // when character runs it reduces distance from pick-up to character by 0.01 and increases distance from arms to pick-up to character by 0.01.
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
