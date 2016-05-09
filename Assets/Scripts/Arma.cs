using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour {

    public int id;
    public int dano;
    public int rango;
    public int amplitud;
    public int velocidadDisparo;
    public Camera camaraPrincipal;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// cambiar para que sus hijos tengas distintos tipos de disparo
    /// </summary>
    void disparar()
    {
        GameObject nuevaBala;//cambiar el resource load por una referencia
        nuevaBala = Instantiate(Resources.Load("Prefabs/spriteBomba"), new Vector2(this.transform.position.x, this.transform.position.y) + getDireccionDisparo() / getDireccionDisparo().magnitude, Quaternion.identity) as GameObject;
        nuevaBala.AddComponent<BoxCollider2D>();
        nuevaBala.AddComponent<Rigidbody2D>();
        nuevaBala.GetComponent<Rigidbody2D>().gravityScale = 0;
        nuevaBala.GetComponent<Rigidbody2D>().AddRelativeForce((getDireccionDisparo() / getDireccionDisparo().magnitude) * velocidadDisparo);
        nuevaBala.tag = "BloqueConstruido";
        nuevaBala.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.3f);
        nuevaBala.transform.localScale = new Vector3(1, 1, 1);
    }

    Vector3 getMousePosition()
    {
        Vector3 mousePos = new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePos;
    }

    Vector2 getDireccionDisparo()
    {
        Vector2 direccion = getMousePosition() - this.transform.position;
        return direccion;
    }

    
}
