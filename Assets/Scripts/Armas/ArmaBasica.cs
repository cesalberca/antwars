using UnityEngine;
using System.Collections;

public class ArmaBasica : MonoBehaviour {

    public int id;
    public int dano;
    public int potencia;
    public int amplitud;
    public float velocidadDisparo;
    public Camera camaraPrincipal;
    public GameObject bala;
    public GameObject jugador;

    void Start()
    {
        //Debug.Log("HOLA");
        //camaraPrincipal = GameObject.Find("Main Camera").GetComponent<Camera>();
        jugador = this.transform.parent.gameObject;
    }
    /// <summary>
    /// cambiar para que sus hijos tengas distintos tipos de disparo
    /// </summary>
    void disparar()
    {
        
    }

    private Vector3 getMousePosition()
    {
        Vector3 mousePos = new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePos;
    }

    public Vector2 getDireccionDisparo()
    {
        Vector2 direccion = getMousePosition() - jugador.transform.position;
        return direccion;
    }

    public void moverArma()
    {
        this.transform.position = new Vector2(jugador.transform.position.x, jugador.transform.position.y) + (getDireccionDisparo() / getDireccionDisparo().magnitude)/3;
    }
    
}
