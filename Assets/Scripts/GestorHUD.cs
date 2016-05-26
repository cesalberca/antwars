using UnityEngine;
using System.Collections;

public class GestorHUD : MonoBehaviour {

    public GameObject salud;
    public GameObject materiales;
    public GameObject jugador;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void refrescar()
    {
        salud.GetComponents<GUIText>().SetValue((jugador.GetComponent<Jugador>().vida), 0);
        materiales.GetComponents<GUIText>().SetValue((jugador.GetComponent<Jugador>().almacenMateriales), 0);
    }
}
