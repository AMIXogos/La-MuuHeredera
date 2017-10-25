using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoboController : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private GameObject zarpa;
    private bool atacando = false;
    public float fuerzaAtaque = 5f;
    public float tiempoAtaque = 0.5f;

    void Start()
    {
        if (zarpa == null)
            zarpa = GameObject.Find("LOBO_GARRA");

        rb = zarpa.GetComponent<Rigidbody>();

        InvokeRepeating("Atacar", 2, 2);
    }

    void Atacar()
    {

        if (atacando == false)
        {

            atacando = true;
            rb.AddForce(-zarpa.transform.forward * fuerzaAtaque, ForceMode.Impulse);
            Invoke("RegresarAtaque", tiempoAtaque);

        }

        Debug.Log("Ataque básico");

    }

    void RegresarAtaque()
    {

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(zarpa.transform.forward * fuerzaAtaque, ForceMode.Impulse);
        Invoke("FinalizarAtaque", tiempoAtaque);

    }

    void FinalizarAtaque()
    {

        atacando = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }

}
