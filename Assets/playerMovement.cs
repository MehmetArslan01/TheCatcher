using UnityEngine;

public class Spieerbewegung : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;
    public float jumpForce;

    private bool canJump;

    private Animator animator;
    private Rigidbody playerRB;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        playerMove();

        float forwardMovement = Input.GetAxis("Vertical");
        animator.SetBool("IsMovingForward", forwardMovement > 0.0f);

    }

    public void playerMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Bewegungsrichtung vorwärts oder rückwärts
        Vector3 moveDirection = Vector3.zero;
        if (verticalInput > 0f)
        {
            moveDirection = transform.forward;
        }
        else if (verticalInput < 0f)
        {
            moveDirection = -transform.forward;
        }

        // Rotation
        float rotationInput = horizontalInput;
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0f, rotationAmount, 0f);

        transform.rotation *= rotation;

        playerRB.velocity = moveDirection * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("terrain"))
    {
        canJump = true;
    }
    else if (collision.gameObject.CompareTag("Player"))
    {
        animator.SetBool("isCollidingWithOtherObject", true);
    }
}

private void OnCollisionExit(Collision collision)
{
    if (collision.gameObject.CompareTag("terrain"))
    {
        canJump = false;
    }
    else if (collision.gameObject.CompareTag("Player"))
    {
        animator.SetBool("isCollidingWithOtherObject", false);
    }
}

}
