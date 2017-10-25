using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZarpaController : MonoBehaviour {

    private GameObject gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("VACA"))
        {
            int danho = gameController.GetComponent<PlayerData>().danhoEnemigos;
            gameController.GetComponent<PlayerData>().vidaJugador = gameController.GetComponent<PlayerData>().vidaJugador - danho;
            Debug.Log("Vida vida: " + gameController.GetComponent<PlayerData>().vidaJugador);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Equals("VACA"))
        {
            int danho = gameController.GetComponent<PlayerData>().danhoEnemigos;
            gameController.GetComponent<PlayerData>().vidaJugador = gameController.GetComponent<PlayerData>().vidaJugador - danho;
            Debug.Log("Vida vaca: " + gameController.GetComponent<PlayerData>().vidaJugador);
        }

    }

}
