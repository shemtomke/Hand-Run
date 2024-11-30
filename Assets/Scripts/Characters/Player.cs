using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterType characterType;
    public float jumpForce = 5f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping = false;
    [SerializeField] bool isDead = false;
    [SerializeField] bool isHit = false;
    public float groundCheckDistance = 0.1f; // Distance for ground check
    public LayerMask groundLayer;            // Layer that represents the ground

    [Header("Sounds")]
    [NonReorderable]
    public List<Sound> sounds = new List<Sound>();
    public AudioClip jumpSound;
    public AudioClip runSound;
    public AudioClip runSound2;
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

        soundManager.PlaySound(soundManager.runningSound, runSound);
        soundManager.PlaySound(soundManager.runningSound2, runSound2);
    }
    private void FixedUpdate()
    {
        GroundCheck();
        Jump();
    }
    private void Update()
    {
        HandleRunningState();
        Death();
    }
    void GroundCheck()
    {
        // Cast a ray downwards from the character's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Draw the ray in the Scene view for debugging
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);

        // Check if the raycast hit something in the ground layer
        isGrounded = hit.collider != null;
    }
    void Jump()
    {
        // player can tap anywhere on screen to jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || IsTouchInputDetected()) && isGrounded)
        {
            // Apply the jump force
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            isJumping = true;

            // Change animation state to jump
            animator.SetBool("IsJumping", isJumping);

            //if (!distanceManager.IsCloseToDoor()) { soundManager.PlaySound(soundManager.jumpingSound, jumpSound); }

            // Play Jump Sound
            soundManager.PlaySound(soundManager.jumpingSound, jumpSound);

            // Stop Playing some run sounds
            // Mosquito
            if (characterType == CharacterType.Mosquito)
            {
                soundManager.PauseSound(soundManager.runningSound2, true);
            }
            // Boy
            else if (characterType == CharacterType.Boy)
            {
                soundManager.PauseSound(soundManager.runningSound, true);
                soundManager.PauseSound(soundManager.runningSound2, true);
            }
        }
    }
    void HandleRunningState()
    {
        if(isGrounded)
        {
            if (!isJumping && !isDead && !isHit)
            {
                animator.SetBool("IsJumping", isJumping);

                // Play The Sounds for running
                soundManager.PauseSound(soundManager.runningSound, false);
                soundManager.PauseSound(soundManager.runningSound2, false);

                //    //if (!distanceManager.IsCloseToDoor())
                //    //{
                //    //    soundManager.PauseSound(soundManager.runningSound, false);
                //    //    soundManager.PauseSound(soundManager.runningSound2, false);
                //    //}
            }
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
        }
    }
    // This method checks if the player is grounded
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Ground") && !isHit && !isDead)
        //{
        //    isJumping = false;

        //    // Pause Sounds when close to the door
        //    //if (!distanceManager.IsCloseToDoor())
        //    //{
        //    //    soundManager.PauseSound(soundManager.runningSound, false);
        //    //    soundManager.PauseSound(soundManager.runningSound2, false);
        //    //}
        //}
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;

            if (characterType == CharacterType.Girl)
            {
                soundManager.PlaySound(soundManager.landingSound, landSound);
            }
        }

        if (collision.gameObject.CompareTag("Flame"))
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

            soundManager.PauseSound(soundManager.runningSound, true);
            soundManager.PauseSound(soundManager.runningSound2, true);
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

        yield return new WaitForSeconds(0.4f);

        isHit = false;
    }
}
