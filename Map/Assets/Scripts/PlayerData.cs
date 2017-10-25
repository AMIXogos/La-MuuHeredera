using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour {

	// VARIABLES

	public int numero_bellotas;
	public Text textBellotas;

	int score;
	Text scoreText;

	public GameObject scoreTextObject;


	public int vidaIncialJugador = 10;
	public int vidaIncialEnemigos = 10;
	[HideInInspector] public float vidaJugador;
	[HideInInspector] public Dictionary<string, float> vidaEnemigos;

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
		scoreText = scoreTextObject.GetComponent<Text>();
		scoreText.text = "Score: " + numero_bellotas.ToString();
		Debug.Log ("Score: " + numero_bellotas.ToString ());
	}

}
