using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToralControllerRespaldo : MonoBehaviour
{
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

    void Start()
    {
        ejeRotacion = Vector3.up;
        animacion = GetComponent<Animator>();
    }

    void Update()
    {
        controlador = GetComponent<CharacterController>();

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

        //Movimiento
        Vector2 inputJoystick = Gamepad.all[0].leftStick.ReadValue();
        Vector3 direccion = new Vector3(inputJoystick.x, 0, inputJoystick.y).normalized;

        if (direccion != Vector3.zero)
        {
            ejeRotacion = Vector3.Cross(direccion, Vector3.up); // Calcular el eje de rotación perpendicular a la dirección de movimiento y el eje y
            transform.rotation = Quaternion.LookRotation(direccion); // Rotar el modelo hacia la dirección de movimiento
        }

        if (controlador.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space) || Gamepad.all[0].buttonSouth.isPressed)
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

        if (Gamepad.all[0].buttonWest.isPressed && puedeGolpear && !estaGolpeando)
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
    }

    IEnumerator Golpe()
    {
        estaGolpeando = true;
        yield return new WaitForSeconds(1f);
        estaGolpeando = false;
    }


}

