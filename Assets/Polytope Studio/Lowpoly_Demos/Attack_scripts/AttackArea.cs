// AttackArea.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player2"))
        {
            // Überprüfe, ob der andere Collider der gewünschte ist
            CapsuleCollider playerCollider = other.GetComponent<CapsuleCollider>();
            if (playerCollider != null && other == playerCollider)
            {
                Debug.Log("Player 1 Attacks and hits" + damage);

                // Füge den Schaden dem anderen Spieler hinzu
                PlayerHealthP2 playerHealth = other.GetComponent<PlayerHealthP2>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }
}
