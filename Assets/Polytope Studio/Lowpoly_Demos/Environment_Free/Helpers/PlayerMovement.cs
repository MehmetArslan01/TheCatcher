using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public AudioSource footstepAudioSource; // Hier fügen wir einen AudioSource hinzu.
    public AudioClip footstepSound; // Hier fügen wir den Schritt-Sound hinzu.

    public AudioClip hitSound;
    private float lastCollisionTime = 0f;


    public float speed = 5;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    bool isMovingForward;
    bool isMovingBackward;

    bool isJumping;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false; // Spieler ist nicht mehr in der Luft
        }

        Debug.Log("isGrounded " + isGrounded + " roundCheck.position " + groundCheck.position + " groundDistance " + groundDistance + " groundMask " + groundMask);

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

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true; // Spieler springt jetzt
            Debug.Log("Springen ausgelöst" + isJumping);
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
        animator.SetBool("isJumping", isJumping); // Hier setzen wir den Animator-Parameter basierend auf isJumping

        // Hier fügen wir den Schritt-Sound hinzu
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
        // Überprüfe, ob mindestens 1 Sekunde seit der letzten Kollision vergangen ist
        if (Time.time - lastCollisionTime >= 5f && other.gameObject.tag == "enemyTree")
        {
            Debug.Log("Kollision mit enemyTree");
            SoundManager.Instance.PlaySound(this.hitSound, other.transform, 1f);
            lastCollisionTime = Time.time;
        }
    }
}
