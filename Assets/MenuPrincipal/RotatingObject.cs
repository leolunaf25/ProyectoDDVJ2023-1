using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        // Raycast desde la cámara hacia el cursor del mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Si el raycast golpea el objeto actual, rota el objeto
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        // Si el objeto no es golpeado por el raycast, vuelve a su orientación original
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
