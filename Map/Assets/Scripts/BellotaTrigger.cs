using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BellotaTrigger : MonoBehaviour {

    private GameObject gameController;

    private void Start() {

        gameController = GameObject.Find("GameController");
        
    }

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.gameObject.name.Equals("VACA")) {

            gameController.GetComponent<PlayerData>().numero_bellotas++;
            Debug.Log(gameController.GetComponent<PlayerData>().numero_bellotas);
            Destroy(gameObject);


        }

    }

}
