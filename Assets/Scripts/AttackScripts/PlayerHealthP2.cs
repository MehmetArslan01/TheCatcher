
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthP2 : MonoBehaviour
{
    private bool canAttack = true;
    public AudioSource audioSource;

    public void TakeDamage(int damage)
    {
        if (PlayerMovementP2.numberOfLeos >= 0 && this.canAttack)
        {
            PlayerMovementP2.numberOfLeos -= damage;
            this.StartCoroutine(this.AttackCooldown());
        }
        Debug.Log(PlayerMovementP2.numberOfLeos + " player2");
        Debug.Log((new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name);

        if (PlayerMovementP2.numberOfLeos < this.GetComponent<PlayerMovementP2>().stackedNPCs.Count)
        {
            this.GetComponent<PlayerMovementP2>().removeNPC();
        }

        // Überprüfe, ob der Spieler tot ist
        if (PlayerMovementP2.numberOfLeos < 0)
        {
            Die();
        }
        audioSource.Play();
    }

    void Die()
    {
        Debug.Log("Player2 died!");
    }

    private IEnumerator AttackCooldown()
    {
        const float cooldown = 1.0f;
        float timer = 0.0f;

        this.canAttack = false;

        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        this.canAttack = true;
    }
}
