using UnityEngine;
using System.Collections;

/// <summary>
/// tipo de arma que dispara proyectiles
/// </summary>
public class ArmaPistola : ArmaBasica {

	// Use this for initialization
	void Start () {
        puedeDisparar = true;
        //recoge los objetos de la escena necesarios
        camaraPrincipal = GameObject.Find("Main Camera").GetComponent<Camera>();
        jugador = GameObject.Find("Jugador").transform.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        moverArma();
	}

    /// <summary>
    /// controla que el arma puede o no disparar
    /// </summary>
    /// <returns>si el arma o no puede disparar</returns>
    bool controlarDisparo()
    {
        if (puedeDisparar)
        {
            disparar();
            return true;
        }
        else return false;
    }

    /// <summary>
    /// dispara el proyectil de este arma cada vez que el calcularRatio, la tasa de fuego, le deja
    /// </summary>
    void disparar()
    {
        this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);    
        puedeDisparar = false;
        GameObject nuevaBala;
        nuevaBala = Instantiate(bala, new Vector2(this.transform.position.x, this.transform.position.y) + (getDireccionDisparo() / getDireccionDisparo().magnitude) / 10, Quaternion.identity) as GameObject;
        nuevaBala.AddComponent<BoxCollider2D>();
        nuevaBala.AddComponent<Rigidbody2D>();
        nuevaBala.GetComponent<Rigidbody2D>().gravityScale = 0;
        nuevaBala.GetComponent<Rigidbody2D>().AddRelativeForce((getDireccionDisparo() / getDireccionDisparo().magnitude) * potencia);
        nuevaBala.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
        if (nuevaBala.GetComponent<Proyectil>())
        {
            nuevaBala.GetComponent<Proyectil>().dano = this.dano;
        }
        StartCoroutine(calcularRatio(velocidadDisparo));
        Destroy(nuevaBala, 10);
    }
}
