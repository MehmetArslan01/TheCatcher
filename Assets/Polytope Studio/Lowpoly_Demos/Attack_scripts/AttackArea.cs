using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2") || other.CompareTag("Player1"))
        {
            // Überprüfe, ob der andere Collider der gewünschte ist
            CapsuleCollider playerCollider = other.GetComponent<CapsuleCollider>();
            if (playerCollider != null && other == playerCollider)
            {
                Debug.Log("Attacked*****");
                // Hier kannst du den Code für den Schaden einfügen, wenn nötig
            }
        }
    }
}
