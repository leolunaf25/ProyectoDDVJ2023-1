using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlPausa1 : MonoBehaviour
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
            if(Gamepad.all[0].startButton.isPressed || Gamepad.all[1].startButton.isPressed)
        {
            canvasPausa.SetActive(true);

        }
        // Si se presiona la tecla "Espacio", activa o desactiva el objeto
        if (Gamepad.all[0].selectButton.isPressed || Gamepad.all[1].selectButton.isPressed)
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
        SceneManager.LoadScene("Historia");
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