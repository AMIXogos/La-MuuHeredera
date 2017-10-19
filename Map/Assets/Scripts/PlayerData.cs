using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	// VARIABLES

	public int numero_bellotas;

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

}
