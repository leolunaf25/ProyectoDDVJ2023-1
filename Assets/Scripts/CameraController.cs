using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string player1Tag = "Player1"; // Etiqueta del jugador 1
    public string player2Tag = "Player2"; // Etiqueta del jugador 2
    public float minDistance = 10f; // Distancia mínima de la cámara a los personajes
    public float maxDistance = 20f; // Distancia máxima de la cámara a los personajes

    private Vector3 midpoint; // Vector para almacenar el punto medio entre los dos personajes
    private GameObject player1Object; // Referencia al GameObject del player1
    private GameObject player2Object; // Referencia al GameObject del player2

    void Start()
    {
        // Buscamos los GameObjects por etiqueta
        player1Object = GameObject.FindWithTag(player1Tag);
        player2Object = GameObject.FindWithTag(player2Tag);
    }

    void FixedUpdate()
    {
        // Calculamos el punto medio entre los personajes
        midpoint = new Vector3((player1Object.transform.position.x + player2Object.transform.position.x) / 2f,
                               (player1Object.transform.position.y + player2Object.transform.position.y) / 2f,
                               (player1Object.transform.position.z + player2Object.transform.position.z) / 2f);

        // Calculamos la distancia entre los personajes
        float distance = Vector3.Distance(player1Object.transform.position, player2Object.transform.position);

        // Ajustamos la distancia de la cámara entre la distancia mínima y máxima
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Calculamos la posición de la cámara ajustando su distancia en el eje Z respecto al punto medio
        Vector3 cameraPosition = midpoint - transform.forward * distance;

        // Ajustamos la posición de la cámara
        transform.position = cameraPosition;

        // Mantenemos la cámara apuntando hacia el punto medio de los personajes
        transform.LookAt(midpoint);
    }
}
