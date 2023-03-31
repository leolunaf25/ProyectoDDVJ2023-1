using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Historia()
    {
        SceneManager.LoadScene("Historia");
    }

    public void Versus()
    {
        SceneManager.LoadScene("Seleccion");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Trofeos()
    {
        SceneManager.LoadScene("Trofeos");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
