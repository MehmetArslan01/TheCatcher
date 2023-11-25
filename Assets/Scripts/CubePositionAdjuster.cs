using UnityEngine;

public class CubePositionAdjuster : MonoBehaviour
{
    public enum Axis { X = 0, Y = 1, Z = 2 } // Enumeration für die Achsen, mit expliziten Werten
    public Axis axisToChange; // Achse, die geändert werden soll, einstellbar im Inspector

    private HUDController hudController;
    public float targetPosition; // Zielposition, einstellbar im Inspector
    private Vector3 startPosition; // Startposition
    private float startTime; // Startzeitpunkt

    void Start()
    {
        // Suche nach dem HUDController im Spiel
        hudController = FindObjectOfType<HUDController>();
        // Speichere die Startposition und Startzeit
        startPosition = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        if (hudController == null) return;

        // Berechne die vergangene Zeit seit dem Start
        float elapsedTime = Time.time - startTime;
        // Gesamtzeit für die Bewegung (Annahme: Gleich der Gesamtspielzeit)
        float totalTime = hudController.matchTimeInMin * 60;

        // Verhindere, dass elapsedTime größer als totalTime wird
        elapsedTime = Mathf.Min(elapsedTime, totalTime);

        // Berechne die neue Position basierend auf der vergangenen Zeit
        float newPosition = Mathf.Lerp(startPosition[(int)axisToChange], targetPosition, elapsedTime / totalTime);
        Vector3 updatedPosition = transform.position;
        updatedPosition[(int)axisToChange] = newPosition; // Aktualisiere die ausgewählte Achse
        transform.position = updatedPosition;
    }
}
