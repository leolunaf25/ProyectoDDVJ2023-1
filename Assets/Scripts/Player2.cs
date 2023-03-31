using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    /*Variables movimientos*/
    public float velocidad = 8f; // Velocidad de movimiento del modelo
    public float fuerzaSalto = 25f; // Fuerza del salto del modelo
    public float gravedad = -50f; // Gravedad del modelo

    private CharacterController controlador; // CharacterController del modelo
    private Vector3 direccion; // Dirección del movimiento
    private Vector3 ejeRotacion; // Eje de rotación de la cara del modelo
    public bool enSuelo = true; // Indica si el modelo está en el suelo
    private float velocidadY = 0f; // Velocidad vertical del modelo
    private Animator animacion;

    private Vector3 movimiento;
    private bool puedeGolpear = true;
    private bool estaGolpeando = false;

    //Lanzar bola
    private GameObject pelota;
    public Transform mano;
    public Transform target;
    private bool sostiene = false;

    /*Variables de salud*/
    public float maxHealth = 100f;
    public float currentHealth;
    public Image imageComponent;

    //HUDS
    private GameObject canvasPausa;

    void Start()
    {
        ejeRotacion = Vector3.up;
        animacion = GetComponent<Animator>();
        currentHealth = maxHealth;
        pelota = GameObject.FindWithTag("Esfera2");
        canvasPausa = GameObject.FindGameObjectWithTag("Pausa");
        imageComponent = GameObject.FindGameObjectWithTag("VidaPlayer2").GetComponent<Image>();

    }

    void Update()
    {
        imageComponent.fillAmount = currentHealth / 100f;
        
        if (Gamepad.all[1].buttonEast.isPressed && sostiene)
        {
            pelota.GetComponent<Rigidbody>().isKinematic = false;
            pelota.GetComponent<Rigidbody>().detectCollisions = true;
            pelota.transform.parent = null;
            Vector3 direction = target.position - pelota.transform.position;
            pelota.GetComponent<Rigidbody>().AddForce(direction.normalized * 1500f);
            animacion.SetBool("lanza", true);
            sostiene = false;
            StartCoroutine(EsperarParaGolpear());
            StartCoroutine(Golpe());
        }
        controlador = GetComponent<CharacterController>();

        //Movimiento
        Vector2 inputJoystick = Gamepad.all[1].leftStick.ReadValue();
        Vector3 direccion = new Vector3(inputJoystick.x, 0, inputJoystick.y).normalized;

        if (direccion != Vector3.zero)
        {
            ejeRotacion = Vector3.Cross(direccion, Vector3.up); // Calcular el eje de rotación perpendicular a la dirección de movimiento y el eje y
            transform.rotation = Quaternion.LookRotation(direccion); // Rotar el modelo hacia la dirección de movimiento
        }

        if (controlador.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) || Gamepad.all[1].buttonSouth.isPressed)
            {
                animacion.SetTrigger("jump");
                velocidadY = fuerzaSalto;

            }
            else
            {
                velocidadY = 0f;
            }
        }
        else
        {
            velocidadY += gravedad * Time.deltaTime;
        }

        if (!estaGolpeando)
        {
            movimiento = direccion * velocidad * Time.deltaTime;
            movimiento.y = velocidadY * Time.deltaTime;
            if ((movimiento.x != 0 || movimiento.z != 0))
            {
                animacion.SetBool("walk", true);
            }
            else
            {
                animacion.SetBool("walk", false);
            }
            controlador.Move(movimiento);
        }
        else
        {
            movimiento = Vector3.zero;
            controlador.Move(movimiento);
        }

        //Combate
        if (Gamepad.all[1].buttonWest.isPressed && puedeGolpear && !estaGolpeando)
        {
            animacion.SetTrigger("punch");
            StartCoroutine(EsperarParaGolpear());
            StartCoroutine(Golpe());
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Plataforma")) return;
        Vector3 normal = hit.normal;
        Vector3 movimientoProyectado = Vector3.ProjectOnPlane(direccion * velocidad * Time.deltaTime, normal);
        controlador.Move(movimientoProyectado);
    }

    IEnumerator EsperarParaGolpear()
    {
        puedeGolpear = false;
        yield return new WaitForSeconds(1f);
        puedeGolpear = true;
        animacion.SetBool("lanza", false);
    }

    IEnumerator Golpe()
    {
        estaGolpeando = true;
        yield return new WaitForSeconds(1f);
        estaGolpeando = false;
    }

    public void TakeDamage(float amount)
    {
            currentHealth -= amount;

        if(currentHealth <= 0)
        {
            imageComponent.fillAmount = 0.0f;
            //canvasPausa.SetActive(false);
            Die();
            DesactivarEsteScript();
        }
    }

    void Die()
    {
        animacion.SetBool("die", true);
    }


    void DesactivarEsteScript()
    {
        MonoBehaviour esteMonoBehaviour = GetComponent<MonoBehaviour>();
        esteMonoBehaviour.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Esfera2"))
        {
            sostiene = true;
            pelota.GetComponent<Rigidbody>().isKinematic = true;
            pelota.transform.parent = mano;
            pelota.transform.localPosition = Vector3.zero;
            pelota.transform.localRotation = Quaternion.identity;
            pelota.GetComponent<Rigidbody>().detectCollisions = false;
        }

        if (other.gameObject.CompareTag("Esfera1"))
        {
            TakeDamage(20);

        }

    }
}

/*
float inputHorizontal = Input.GetAxis("Horizontal");
float inputVertical = Input.GetAxis("Vertical");
direccion = new Vector3(inputHorizontal, 0, inputVertical).normalized;


Vector3 direccion = Vector3.zero;

if (Input.GetKey(KeyCode.A))
{
    direccion += Vector3.left;
}

if (Input.GetKey(KeyCode.D))
{
    direccion += Vector3.right;
}

if (Input.GetKey(KeyCode.W))
{
    direccion += Vector3.forward;
}

if (Input.GetKey(KeyCode.S))
{
    direccion += Vector3.back;
}

direccion = direccion.normalized;
 */