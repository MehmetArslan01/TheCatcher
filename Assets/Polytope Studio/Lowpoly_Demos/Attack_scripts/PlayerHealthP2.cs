// PlayerHealth.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthP2 : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        PlayerMovementP2.numberOfLeos -= damage;

        Debug.Log(PlayerMovementP2.numberOfLeos + " player2");


        // Überprüfe, ob der Spieler tot ist
        if (PlayerMovementP2.numberOfLeos < 0)
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
