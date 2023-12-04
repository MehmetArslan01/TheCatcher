using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacterPlacement : MonoBehaviour
{
    public string characterTag = "YourCharacterTag";
    public int numberOfCharacters = 10; // Anzahl der zu duplizierenden Charaktere.
    public float groundHeight = 0.5f; // Höhe über dem Boden, auf der die Charaktere platziert werden sollen.

    void Start()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag(characterTag);

        for (int i = 0; i < numberOfCharacters; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-100f, 100f), // X-Koordinate im Bereich von -100 bis 100.
                groundHeight, // Höhe über dem Boden.
                Random.Range(-73.1f, 126.9f) // Z-Koordinate im Bereich von -73.1 bis 126.9.
            );

            int randomCharacterIndex = Random.Range(0, characters.Length);

            GameObject newCharacter = Instantiate(characters[randomCharacterIndex], randomPosition, Quaternion.identity);

        }
    }
}
