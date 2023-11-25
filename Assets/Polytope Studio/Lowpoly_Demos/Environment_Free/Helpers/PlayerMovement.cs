using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;
    public AudioClip hitSound;
    private float lastCollisionTime = 0f;
    public float speed = 4;
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

    private float lastLeoCollisionTime = 0f;
    private float leoCollisionDelay = 3f;
    public static int numberOfLeos;

    public float leoHeight;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterCollider = GetComponent<CapsuleCollider>();
        numberOfLeos = 0;
    }

    void Update()
    {
        // Überprüfen, ob der Charakter am Boden ist
        isGrounded = CheckIfGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        transform.Rotate(Vector3.up * x * rotationSpeed * Time.deltaTime);

        // Sprunglogik
        if (Input.GetButtonDown("Jump2") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Anwenden der Schwerkraft
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Animationstrigger für Vorwärts- und Rückwärtsbewegung
        isMovingForward = z > 0;
        isMovingBackward = z < 0;

        animator.SetBool("runForward", isMovingForward);
        animator.SetBool("runBackward", isMovingBackward);
        animator.SetBool("isJumping", !isGrounded && velocity.y > 0);

        // Angriffslogik
        if (Input.GetButtonDown("Fire2") && !isAttacking)
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

        // Fußschritt-Audios
        if (isGrounded && (isMovingForward || isMovingBackward) && !footstepAudioSource.isPlaying)
        {
            footstepAudioSource.clip = footstepSound;
            footstepAudioSource.Play();
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        attackTimer = 0f;
        animator.SetBool("isAttacking", true);
        // Hier können Sie die Logik für den Angriff implementieren
    }

    void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    bool CheckIfGrounded()
    {
        float rayLength = 0.1f; // Länge der Raycast-Linie
        float slopeLimit = controller.slopeLimit; // Der maximale Winkel, den der Charakter hochgehen kann

        RaycastHit hit;

        // Überprüfen, ob die Raycast-Linie etwas trifft
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, rayLength, groundMask))
        {
            // Überprüfen, ob die Oberfläche geneigt ist
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle <= slopeLimit)
            {
                return true; // Der Charakter steht auf einem nicht zu steilen Untergrund
            }
        }

        return false; // Der Charakter steht nicht auf dem Boden oder auf einem zu steilen Untergrund
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Leo" && Time.time - lastLeoCollisionTime >= leoCollisionDelay)
        {
            if (!isAttacking)
            {
                StartAttack();
            }

            NPCController leoController = other.gameObject.GetComponent<NPCController>();
            if (leoController != null)
            {
                leoController.GetCaught();
            }

            Transform leoTransform = other.transform;
            Vector3 stackPositionOffset = new Vector3(0, (characterCollider.height - 0.6f) + ((characterCollider.height - 1.3f) * numberOfLeos), 0);
            Vector3 characterTopPosition = transform.position + stackPositionOffset;
            leoTransform.position = characterTopPosition;
            leoTransform.rotation = transform.rotation * Quaternion.Euler(20f, 0f, 0f);

            leoTransform.SetParent(transform);

            Rigidbody leoRigidbody = leoTransform.GetComponent<Rigidbody>();
            if (leoRigidbody != null)
            {
                Destroy(leoRigidbody); // Entfernen Sie den Rigidbody vollständig
            }

            numberOfLeos++;
            lastLeoCollisionTime = Time.time;
            other.gameObject.tag = "Untagged";

            // Slow the player with each NPCs
            speed = (float)(speed - (0.2 * numberOfLeos));
        }
    }
}
