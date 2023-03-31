using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombateController : MonoBehaviour
{

    public GameObject objeto10;
    public GameObject objeto11;
    public GameObject objeto12;
    public GameObject objeto13;
    public GameObject objeto14;
    public GameObject objeto15;

    public Sprite imagen1;
    public Sprite imagen2;
    public Sprite imagen3;
    public Sprite imagen4;
    public Sprite imagen5;
    public Sprite imagen6;

    public GameObject objeto20;
    public GameObject objeto21;
    public GameObject objeto22;
    public GameObject objeto23;
    public GameObject objeto24;
    public GameObject objeto25;

    public Transform[] AparecePlayer1;
    public Transform[] AparecePlayer2;

    public Image fotoPlayer1;
    public Image fotoPlayer2;


    void Start()
    {
        fotoPlayer1 = GameObject.FindGameObjectWithTag("Perfil1").GetComponent<Image>();
        fotoPlayer2 = GameObject.FindGameObjectWithTag("Perfil2").GetComponent<Image>();

        string selectedPlayer1 = PlayerPrefs.GetString("selectedPlayer1");
        string selectedPlayer2 = PlayerPrefs.GetString("selectedPlayer2");
        Debug.Log("EL ENFRENTAMIENTO SERA: " + selectedPlayer1 + " VS " + selectedPlayer2);

        Transform puntoAleatorio = AparecePlayer1[Random.Range(0, AparecePlayer1.Length)];

        Transform puntoAleatorio2 = AparecePlayer2[Random.Range(0, AparecePlayer2.Length)];

        // Seleccionar Player 1
        GameObject objeto;
        switch (selectedPlayer1)
        {
            case "P10":
                objeto = objeto10;
                fotoPlayer1.sprite = imagen1;
                break;
            case "P11":
                objeto = objeto11;
                fotoPlayer1.sprite = imagen2;
                break;
            case "P12":
                objeto = objeto12;
                fotoPlayer1.sprite = imagen3;
                break;
            case "P13":
                objeto = objeto13;
                fotoPlayer1.sprite = imagen4;
                break;
            case "P14":
                objeto = objeto14;
                fotoPlayer1.sprite = imagen5;
                break;
            case "P15":
                objeto = objeto15;
                fotoPlayer1.sprite = imagen6;
                break;
            default:
                Debug.LogError("Argumento inválido");
                return;
        }

        Instantiate(objeto, puntoAleatorio.position, puntoAleatorio.rotation);

        // Seleccionar Player 1
        GameObject objeto2;
        switch (selectedPlayer2)
        {
            case "P20":
                objeto2 = objeto20;
                fotoPlayer2.sprite = imagen6;
                break;
            case "P21":
                objeto2 = objeto21;
                fotoPlayer2.sprite = imagen5;
                break;
            case "P22":
                objeto2 = objeto22;
                fotoPlayer2.sprite = imagen4;
                break;
            case "P23":
                objeto2 = objeto23;
                fotoPlayer2.sprite = imagen3;
                break;
            case "P24":
                objeto2 = objeto24;
                fotoPlayer2.sprite = imagen2;
                break;
            case "P25":
                objeto2 = objeto25;
                fotoPlayer2.sprite = imagen1;
                break;
            default:
                Debug.LogError("Argumento inválido");
                return;
        }
        Instantiate(objeto2, puntoAleatorio2.position, puntoAleatorio2.rotation);

    }
}
    