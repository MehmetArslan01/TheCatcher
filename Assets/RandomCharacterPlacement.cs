using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacterPlacement : MonoBehaviour
{
    public string characterTag = "YourCharacterTag"; // Das Tag des Charakters, den Sie duplizieren möchten.
    public int numberOfCharacters = 10; // Anzahl der zu duplizierenden Charaktere.
    public float groundHeight = 0.5f; // Höhe über dem Boden, auf der die Charaktere platziert werden sollen.

    void Start()
    {
        // Finden Sie alle Game-Objekte mit dem angegebenen Tag.
        GameObject[] characters = GameObject.FindGameObjectsWithTag(characterTag);

        for (int i = 0; i < numberOfCharacters; i++)
        {
            // Wählen Sie eine zufällige Position für die Platzierung aus.
            Vector3 randomPosition = new Vector3(
                Random.Range(-50f, 50f), // Passen Sie die gewünschten Bereiche an.
                groundHeight, // Höhe über dem Boden.
                Random.Range(-50f, 50f)
            );

            // Wählen Sie zufällig einen Charakter aus dem Array aus.
            int randomCharacterIndex = Random.Range(0, characters.Length);

            // Erstellen Sie eine Kopie des ausgewählten Charakters.
            GameObject newCharacter = Instantiate(characters[randomCharacterIndex], randomPosition, Quaternion.identity);

            // Aktivieren Sie die Animation für den erstellten Charakter.
            

            // Zeigen Sie eine Debug-Nachricht an, um zu bestätigen, dass der Charakter erstellt wurde.
            Debug.Log("Character created at position: " + randomPosition);
        }
    }
}
