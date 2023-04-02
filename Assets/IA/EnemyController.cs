using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator EnemyAnimator;

    public GameObject jugador;

    public float distanciaMinima = 1f;
    public float velocidadMovimiento = 2f;
    public float fuerzaAtaque = 10f;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        // Calcular la distancia
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);

        // Si el jugador está lo suficientemente cerca, atacar
        if (distancia < distanciaMinima)
        {
            // Atacar al jugador
            EnemyAnimator.SetTrigger("Punch");
        }

        if (distancia > 5.0f)
        {
            Debug.Log("Estamos lejos");

        }

        if (distancia < 5f)
        {
            Debug.Log("Estamos cerca");
        }

        /*
        else
        {
            // Seguir al jugador
            // Obtener la posición actual del objeto
            Vector3 currentPosition = transform.position;

            // Crear un nuevo vector de destino con el mismo valor en X y Z que el punto de destino, pero con la posición actual del objeto en el eje Y
            Vector3 newDestination = new Vector3(jugador.transform.position.x, currentPosition.y, jugador.transform.position.z);

            // Orientar el objeto hacia el nuevo vector de destino, ignorando el eje Y
            transform.LookAt(newDestination);

            transform.Translate(Vector3.forward * velocidadMovimiento * Time.deltaTime);
            EnemyAnimator.SetTrigger("Walk");
        }*/
    }

}
