// PlayerHealth.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private bool canAttack = true;
    public AudioSource audioSource;

    public void TakeDamage(int damage)
    {
        if (PlayerMovement.numberOfLeos >= 0 && this.canAttack)
        {
            PlayerMovement.numberOfLeos -= damage;
            this.StartCoroutine(this.AttackCooldown());
        }

        Debug.Log(PlayerMovement.numberOfLeos + " player1");

        if (PlayerMovement.numberOfLeos < this.GetComponent<PlayerMovement>().stackedNPCs.Count)
        {
            this.GetComponent<PlayerMovement>().removeNPC();
        }

        // Überprüfe, ob der Spieler tot ist
        if (PlayerMovement.numberOfLeos < 0)
        {
            Die();
        }
        audioSource.Play();
    }

    void Die()
    {
        // Hier kannst du den Code für den Tod des Spielers einfügen
        Debug.Log("Player1 died!");
    }

    private IEnumerator AttackCooldown()
    {
        const float cooldown = 1.0f;
        float timer = 0.0f;

        this.canAttack = false;

        while(timer < cooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        this.canAttack = true;
    }
}
