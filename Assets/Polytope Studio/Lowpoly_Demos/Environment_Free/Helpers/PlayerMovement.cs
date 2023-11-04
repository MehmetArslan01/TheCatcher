using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;

    public AudioClip hitSound;
    private float lastCollisionTime = 0f;

    public float speed = 5;
    public float rotationSpeed = 90f; // Adjust the rotation speed as needed
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    private CapsuleCollider characterCollider;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool isMovingForward;
    bool isMovingBackward;

    bool isJumping;

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

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Rotation based on "A" and "D" keys
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

        // Add footstep sound
        if (isGrounded && (isMovingForward || isMovingBackward))
        {
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = footstepSound;
                footstepAudioSource.Play();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Time.time - lastCollisionTime >= 5f && other.gameObject.tag == "enemyTree")
        {
            Debug.Log("Collision with enemyTree");
            SoundManager.Instance.PlaySound(this.hitSound, other.transform, 1f);
            lastCollisionTime = Time.time;
        }
    }
}
