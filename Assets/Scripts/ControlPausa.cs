using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlPausa : MonoBehaviour
{
    public GameObject canvasPausa;

    void Start()
    {
        // Desactiva el objeto al inicio del juego
        canvasPausa.SetActive(false);
    }

    void Update()
    {
        // Si se presiona la tecla "Espacio", activa o desactiva el objeto
            if(Input.GetKey(KeyCode.T))
        {
            canvasPausa.SetActive(true);

        }
        // Si se presiona la tecla "Espacio", activa o desactiva el objeto
        if (Input.GetKey(KeyCode.Y))
        {
            canvasPausa.SetActive(false);

        }

    }


    public void MenuPrincipal()
    {
        SceneManager.LoadScene("Principal");
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Combate");
    }

    public void Seleccionar()
    {
        SceneManager.LoadScene("Seleccion");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}