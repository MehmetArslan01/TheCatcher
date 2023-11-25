// PlayerHealth.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        PlayerMovement.numberOfLeos -= damage;

        Debug.Log(PlayerMovement.numberOfLeos + " player1");

        // Überprüfe, ob der Spieler tot ist
        if (PlayerMovement.numberOfLeos < 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Hier kannst du den Code für den Tod des Spielers einfügen
        Debug.Log("Player1 died!");
    }
}
