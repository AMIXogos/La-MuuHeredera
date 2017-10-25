using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour {

	// VARIABLES

	public int numero_bellotas;
	public Text textBellotas;

    [HideInInspector] int score;
    [HideInInspector] Text scoreText;
	public GameObject scoreTextObject;

	public int vidaIncialJugador = 10;
	public int vidaIncialEnemigos = 10;
    public int danhoJugador = 1;
    public int danhoEnemigos = 1;
	[HideInInspector] public float vidaJugador;
	[HideInInspector] public Dictionary<string, float> vidaEnemigos;
    [HideInInspector] public float vidaTemporalEnemigo = 10; // BORRAR

	// FUNCIONES

	void Start()
	{

		// No eliminar

		DontDestroyOnLoad(this);

		// Inicialización de variables

		numero_bellotas = 0;	

		vidaJugador = vidaIncialJugador;
		vidaEnemigos = new Dictionary<string, float>();

	}

	void Update()
	{
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (scoreText == null) {
                scoreTextObject = GameObject.Find("Score");
            }

            scoreText = scoreTextObject.GetComponent<Text>();
            scoreText.text = "Score: " + numero_bellotas.ToString();
            Debug.Log("Score: " + numero_bellotas.ToString());
        }
	}

}
