// AttackArea.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaP2: MonoBehaviour
{
    private int damage = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Überprüfe, ob der andere Collider der gewünschte ist
            CapsuleCollider playerCollider = other.GetComponent<CapsuleCollider>();
            if (playerCollider != null && other == playerCollider)
            {
                Debug.Log("Player 2 Attacks and hits");

                // Füge den Schaden dem anderen Spieler hinzu
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }
}
