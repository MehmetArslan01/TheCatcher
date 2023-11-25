using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaP2 : MonoBehaviour
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
                Debug.Log("Player2 Attacks and hits");
                // Hier kannst du den Code für den Schaden einfügen, wenn nötig
            }
        }
    }
}
