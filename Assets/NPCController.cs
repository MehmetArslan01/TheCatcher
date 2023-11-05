using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class NPCController : MonoBehaviour
{

    public float speed = 5;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;
    public float fleeDistance = 10;
    public Transform player;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;
    bool isFleeing = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        StartCoroutine(NewHeading());
    }

    void Update()
    {
        //CheckForPlayer();
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        controller.SimpleMove(forward * speed);
    }

    IEnumerator NewHeading()
    {
        while (true)
        {
            if (!isFleeing)  // Only change heading if not fleeing
            {
                NewHeadingRoutine();
            }
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    void NewHeadingRoutine()
    {
        var floor = Mathf.Clamp(transform.eulerAngles.y - maxHeadingChange, 0, 360);
        var ceil = Mathf.Clamp(transform.eulerAngles.y + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    void CheckForPlayer()
    {
        Vector3 toPlayer = player.position - transform.position;
        if (toPlayer.magnitude < fleeDistance)
        {
            // Fliehe vom Spieler
            Vector3 awayFromPlayer = transform.position - player.position;
            targetRotation = Quaternion.LookRotation(awayFromPlayer).eulerAngles;
            isFleeing = true;
        }
        else
        {
            isFleeing = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            // Ändere die Richtung beim Kollidieren mit einem Border-Objekt
            heading = transform.eulerAngles.y + 180;
            targetRotation = new Vector3(0, heading, 0);
            Debug.Log("Collison: " + targetRotation);
        }
    }
}
