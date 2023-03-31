using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionPlayers : MonoBehaviour
{
    public string selectedPlayer1; // Variable que guarda el nombre del cubo seleccionado
    public string selectedPlayer2; // Variable que guarda el nombre del cubo seleccionado

    public GameObject[] cubeObjects;
    public GameObject[] cubeObjects2;
    public bool player1 = false;
    public bool player2 = false;
    
    private void Update()
    {
        if (player1 && player2)
        {
            Invoke("CargarSiguienteEScena", 3f);
        }
    }

    void CargarSiguienteEScena()
    {
        PlayerPrefs.SetString("selectedPlayer1", selectedPlayer1);
        PlayerPrefs.SetString("selectedPlayer2", selectedPlayer2);
        SceneManager.LoadScene("Combate", LoadSceneMode.Single);
    }


    public void SetselectedPlayer1(string cubeName)
    {
        selectedPlayer1 = cubeName; // Guardar el nombre del cubo seleccionado
        Debug.Log("Se ha seleccionado el personaje" + selectedPlayer1);
        DisableAllCubeScripts1();
        player1 = true;
    }

    public void SetselectedPlayer2(string cubeName)
    {
        selectedPlayer2 = cubeName; // Guardar el nombre del cubo seleccionado
        Debug.Log("Se ha seleccionado el personaje" + selectedPlayer2);
        DisableAllCubeScripts2();
        player2 = true;
    }

    public void DisableAllCubeScripts1()
    {
        foreach (GameObject cubeObject in cubeObjects)
        {
            ControllerP1 cubeScript = cubeObject.GetComponent<ControllerP1>();
            if (cubeScript != null)
            {
                cubeScript.enabled = false;
            }
        }
    }

    public void DisableAllCubeScripts2()
    {
        foreach (GameObject cubeObject in cubeObjects2)
        {
            ControllerP2 cubeScript = cubeObject.GetComponent<ControllerP2>();
            if (cubeScript != null)
            {
                cubeScript.enabled = false;
            }
        }
    }
}

