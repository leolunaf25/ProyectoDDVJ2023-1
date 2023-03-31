using UnityEngine;

public class ControllerP2 : MonoBehaviour
{
    public float rotateSpeed = 100f; // Velocidad de rotación
    private bool isMouseOver = false; // Variable que indica si el mouse está sobre el cubo
    private Quaternion originalRotation; // Rotación original del cubo
    private bool isClicked = false; // Variable que indica si el cubo ha sido clickeado
    public Transform targetPosition; // Posición objetivo del cubo
    public SelectionPlayers selectionController; // Controlador de selección

    void Start()
    {
        originalRotation = transform.rotation; // Guardar la rotación original del cubo
    }

    void Update()
    {
        if (isClicked) // Si el cubo ha sido clickeado
        {
            return; // Salir del Update para evitar que el cubo siga girando
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Lanzar un rayo desde la posición del mouse

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject == gameObject) // Si el rayo impacta con este cubo
            {
                if (!isMouseOver) // Si el mouse no estaba sobre el cubo antes
                {
                    isMouseOver = true; // Cambiar el estado de la variable
                }
                transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime); // Girar el cubo sobre su propio eje
            }
            else
            {
                if (isMouseOver) // Si el mouse estaba sobre el cubo antes
                {
                    isMouseOver = false; // Cambiar el estado de la variable
                    transform.rotation = originalRotation; // Regresar el cubo a su rotación original
                }
            }
        }//
        else
        {
            if (isMouseOver) // Si el mouse estaba sobre el cubo antes
            {
                isMouseOver = false; // Cambiar el estado de la variable
                transform.rotation = originalRotation; // Regresar el cubo a su rotación original
            }
        }

        if (Input.GetMouseButtonDown(0) && isMouseOver) // Si se hace clic izquierdo y el mouse está sobre el cubo
        {
            isClicked = true; // Cambiar el estado de la variable
            transform.rotation = originalRotation;
            transform.position = targetPosition.position; // Mover el cubo a la posición objetivo
            selectionController.SetselectedPlayer2(gameObject.name); // Guardar el nombre del objeto (cubo) clickeado en el controlador de selección
        }
    }
}
