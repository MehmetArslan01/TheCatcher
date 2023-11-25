// PlayerHealth.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Überprüfe, ob der Spieler tot ist
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Hier kannst du den Code für den Tod des Spielers einfügen
        Debug.Log("Player died!");
    }
}
