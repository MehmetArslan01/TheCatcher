using System.Collections;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerMovementP2 : NetworkBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;
    public AudioClip hitSound;
    private float lastCollisionTime = 0f;
    public float speed = 5;
    public float rotationSpeed = 90f;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;
    public float attackDuration = 0.5f;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    public Camera playerCamera;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    private CapsuleCollider characterCollider;
    Vector3 velocity;
    bool isGrounded;
    bool isMovingForward;
    bool isMovingBackward;
    bool isJumping;

    private string horizontalInput;
    private string verticalInput;
    private string jumpInput;
    private string attackInput;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            //    gameObject.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        characterCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        isGrounded = IsGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5f;
            isJumping = false;
        }

        if (Input.GetKey("left shift") && isGrounded)
        {
            speed = 10;
        }
        else
        {
            speed = 5;
        }

        float x = Input.GetAxis("HorizontalSecond");
        float z = Input.GetAxis("VerticalSecond");

        Vector3 move = transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        transform.Rotate(Vector3.up * x * rotationSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
            Debug.Log("Jump triggered: " + isJumping);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (z > 0)
        {
            isMovingForward = true;
            isMovingBackward = false;
        }
        else if (z < 0)
        {
            isMovingBackward = true;
            isMovingForward = false;
        }
        else
        {
            isMovingForward = false;
            isMovingBackward = false;
        }

        animator.SetBool("runForward", isMovingForward);
        animator.SetBool("runBackward", isMovingBackward);
        animator.SetBool("isJumping", isJumping);

        // Angriff auslösen, wenn "Fire1" (z.B., linke Maustaste) gedrückt wird
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            StartAttack();
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                EndAttack();
            }
        }

        if (isGrounded && (isMovingForward || isMovingBackward))
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = footstepSound;
                footstepAudioSource.Play();
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;
        Debug.Log("Attack started");
        animator.SetBool("isAttacking", true);
        // Hier können Sie die Logik für den Angriff implementieren
    }

    void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Attack ended");
        animator.SetBool("isAttacking", false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Time.time - lastCollisionTime >= 5f && other.gameObject.tag == "enemyTree")
        {
            Debug.Log("Collision with enemyTree");
            // SoundManager.Instance.PlaySound(this.hitSound, other.transform, 1f);
            lastCollisionTime = Time.time;
        }
    }
}
