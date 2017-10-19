using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour {

    // TIPOS

    public enum DireccionEsquivar { Aleatorio, Atras, Derecha, Izquierda };

    // VARIABLES

    public GameObject cameraContainer;

    private Rigidbody rb;
    private bool atacando = false;
    public float fuerzaAtaque = 5f;
    public float tiempoAtaque = 0.5f;
    private bool esquivando = false;
    public DireccionEsquivar direccionEsquivar = DireccionEsquivar.Aleatorio;
    private Vector3 vectorEsquivar;
    public float fuerzaEsquive = 5f;
    public float tiempoEsquive = 0.5f;

    public float rotacion = 100f;
    public float movimiento = 7.5f;

    private Vector2 rotacionSmoothVector;
    private Vector2 rotacionMouseLook;
    private float rotacionTiempoRestante;
    public float rotacionTiempoRestablecer = 0.25f;
    public float rotacionSensibilidad = 5f;
    public float rotacionSuavidad = 2f;

    public float zoomSensibilidad = 10f;
    public float zoomMin = 15f;
    public float zoomMax = 90f;

    // FUNCIONES

    void Start () {

        rb = GetComponent<Rigidbody>();

        if (cameraContainer == null)
            cameraContainer = GameObject.Find("Camera Container");

    }
    
    void Update () {

        // SALIR

        if (Input.GetKeyDown(KeyCode.Escape)) {
            
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif

        }

        /* Aquí habría que presentar el menú */

        // MOVIMIENTO

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * movimiento * Time.deltaTime);

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector3.back * movimiento * Time.deltaTime);

        /*transform.Translate(0, 0, Input.GetAxis("Vertical") * movimiento * Time.deltaTime);*/

        // SALTAR

        /* ¿Las vacas saltan? */

        // ROTACIÓN

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(new Vector3(0f, -rotacion, 0f) * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(new Vector3(0f, rotacion, 0f) * Time.deltaTime);

        // CÁMARA

        if (Input.GetMouseButton(0)) {
            
            Vector2 ratonAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            ratonAxis = Vector2.Scale(ratonAxis, new Vector2(rotacionSensibilidad * rotacionSuavidad, rotacionSensibilidad * rotacionSuavidad));
            rotacionSmoothVector.x = Mathf.Lerp(rotacionSmoothVector.x, ratonAxis.x, 1f / rotacionSuavidad);
            rotacionSmoothVector.y = Mathf.Lerp(rotacionSmoothVector.y, ratonAxis.y, 1f / rotacionSuavidad);

            rotacionMouseLook += rotacionSmoothVector;
            rotacionMouseLook.y = Mathf.Clamp(rotacionMouseLook.y, -90f, 90f);

            cameraContainer.transform.localRotation = Quaternion.AngleAxis(-rotacionMouseLook.y, Vector3.right);
            cameraContainer.transform.localRotation = Quaternion.AngleAxis(rotacionMouseLook.x, transform.up);

            rotacionTiempoRestante = rotacionTiempoRestablecer;
        
        } else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            
            rotacionTiempoRestante -= Time.deltaTime;

            if (rotacionTiempoRestante < 0) {
                cameraContainer.transform.localRotation = Quaternion.Lerp(cameraContainer.transform.localRotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
            }
        }

        // ZOOM

        float zoom = Camera.main.fieldOfView;
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensibilidad;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
        Camera.main.fieldOfView = zoom;

        // BOTONES DE ACCION

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.J))
            Esquivar();

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.K))
            Atacar();

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.L))
            Debug.Log("Ataque especial");

        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.I))
            Debug.Log("Inventario");

    }

    void Esquivar() {

        if (atacando == false && esquivando == false)
        {

            switch (direccionEsquivar) {

                case DireccionEsquivar.Aleatorio:
                    int n = Random.Range(0, 3);
                    switch (n) {
                        case 0:
                            vectorEsquivar = -transform.forward;
                            break;
                        case 1:
                            vectorEsquivar = transform.right;
                            break;
                        case 2:
                            vectorEsquivar = -transform.right;
                            break;
                        default:
                            vectorEsquivar = Vector3.zero;
                            break;
                    }
                    break;

                case DireccionEsquivar.Atras:
                    vectorEsquivar = -transform.forward;
                    break;

                case DireccionEsquivar.Derecha:
                    vectorEsquivar = transform.right;
                    break;

                case DireccionEsquivar.Izquierda:
                    vectorEsquivar = -transform.right;
                    break;

                default:
                    vectorEsquivar = Vector3.zero;
                    break;

            }

            esquivando = true;
            rb.AddForce(vectorEsquivar * fuerzaEsquive, ForceMode.Impulse);
            Invoke("RegresarEsquive", tiempoEsquive);

        }

        Debug.Log("Esquivar");

    }

    void RegresarEsquive () {

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(-vectorEsquivar * fuerzaEsquive, ForceMode.Impulse);
        Invoke("FinalizarEsquive", tiempoEsquive);

    }

    void FinalizarEsquive() {

        esquivando = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

    void Atacar() {

        if (atacando == false && esquivando == false) {

            atacando = true;
            rb.AddForce(transform.forward * fuerzaAtaque, ForceMode.Impulse);
            Invoke("RegresarAtaque", tiempoAtaque);

        }

        Debug.Log("Ataque básico");

    }

    void RegresarAtaque() {
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(-transform.forward * fuerzaAtaque, ForceMode.Impulse);
        Invoke("FinalizarAtaque", tiempoAtaque);

    }

    void FinalizarAtaque() {
        
        atacando = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

}
