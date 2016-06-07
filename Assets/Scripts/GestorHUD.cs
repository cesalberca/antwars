using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// el gestor del hud que se muestra
/// </summary>
public class GestorHUD : MonoBehaviour
{

    public Text salud;                      //el texto que muestra la salud del jugador
    public Text materiales;                 //el texto que muestra los materiales del jugador
    public GameObject jugador;              //el jugador
    public GameObject baseOperaciones;      //la base que el jugador debe proteger

    public List<Button> almacenBotones;     //todos los botones que sirven para cambiar armas
    public Text textoMuerte;                //el texto que se muestra cuando mueres
    public Text textoBase;                  //el texto que se muestra cuando tu base es detruida
    public Button reiniciar;                //el boton de reinicio
    public Button menu;                     //el boton de volver al menu

    private bool estadoBotones;
    private int vida;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mostrarBotones();
        controlarMuerte();
        refrescar();
    }

    /// <summary>
    /// refresca los valores de los textos de salud y materiales
    /// </summary>
    public void refrescar()
    {
        if (jugador != null)
        {
            salud.text = "SALUD " + jugador.GetComponent<Jugador>().vida;
            materiales.text = "MATERIALES " + jugador.GetComponent<Jugador>().almacenMateriales;
        }
    }
    /// <summary>
    /// controla que se muestren los objetos correctos cuando el jugador o la base mueren
    /// </summary>
    void controlarMuerte()
    {
        vida = jugador.GetComponent<Jugador>().vida;
        if (vida <= 0)
        {
            textoMuerte.transform.gameObject.SetActive(true);
            reiniciar.transform.gameObject.SetActive(true);
            menu.transform.gameObject.SetActive(true);
        }

        //aqui va un if que comprueba que la vida de la base es mayor que 0
    }

    #region funciones de los botones de seleccion de arma
    /// <summary>
    /// controla la pulsacion del boton de mostrar el menu de seleccion de arma
    /// </summary>
    void mostrarBotones()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controlarBotones();
        }
    }
    /// <summary>
    /// controla que el menu de seleccion de arma sea visible o no
    /// </summary>
    public void controlarBotones()
    {
        if (estadoBotones == true)
        {
            enableAlmacenBotones(false);
        }
        else if (estadoBotones == false)
        {
            enableAlmacenBotones(true);
        }
    }

    /// <summary>
    /// cambia el estado de los botones del almacen de botones de eleccion de armas
    /// </summary>
    /// <param name="estado"> el estado que queremos darle a los botones</param>
    void enableAlmacenBotones(bool estado)
    {
        for (int i = 0; i < almacenBotones.Count; i++)
        {
            almacenBotones[i].transform.gameObject.SetActive(estado);
            estadoBotones = estado;
        }
    }
    #endregion
    #region funciones del menu de muerte
    /// <summary>
    /// boton de eleccion de volver al menu principal
    /// </summary>
    public void irMenu()
    {
        SceneManager.LoadScene("menuPrincipal");
    }

    /// <summary>
    /// boton de eleccion de reiniciar el juego
    /// </summary>
    public void reiniciarMapa()
    {
        SceneManager.LoadScene("juegoPrincipal");
    }
    #endregion
}
