using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GestorHUD : MonoBehaviour {

    public Text salud;
    public Text materiales;
    public GameObject jugador;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void refrescar()
    {
        salud.text = "SALUD " + jugador.GetComponent<Jugador>().vida;
        materiales.text = "MATERIALES " + jugador.GetComponent<Jugador>().almacenMateriales;
    }
}
