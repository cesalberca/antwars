using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GestorHUD : MonoBehaviour {

    public Text salud;
    public Text materiales;
    public GameObject jugador;

    public List<Button> almacenBotones;

    private bool estadoBotones;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        mostrarBotones();
    }

    public void refrescar()
    {
        salud.text = "SALUD " + jugador.GetComponent<Jugador>().vida;
        materiales.text = "MATERIALES " + jugador.GetComponent<Jugador>().almacenMateriales;
    }

    void mostrarBotones()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controlarBotones();
        }
    }
    public void controlarBotones()
    {
        if (estadoBotones == true)
        {
            enableAlmacenBotones(false);
        } else if (estadoBotones == false)
        {
            enableAlmacenBotones(true);
        }
    }

    void enableAlmacenBotones(bool estado)
    {
        for (int i = 0; i < almacenBotones.Count; i++)
        {
            Debug.Log("hola" + i);
            almacenBotones[i].transform.gameObject.SetActive(estado);
            estadoBotones = estado;
        }
    }
}
