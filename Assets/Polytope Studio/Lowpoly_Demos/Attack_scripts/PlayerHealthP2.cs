// PlayerHealth.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthP2 : MonoBehaviour
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
                Debug.Log(currentHealth + " player2");


        // Überprüfe, ob der Spieler tot ist
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Hier kannst du den Code für den Tod des Spielers einfügen
        Debug.Log("Player2 died!");
    }
}
