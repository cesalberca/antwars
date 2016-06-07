using UnityEngine;
using System.Collections;

/// <summary>
/// el arma lanzallamas
/// </summary>
public class ArmaLanzallamas : ArmaBasica
{
    // Use this for initialization
    void Start()
    {
        puedeDisparar = true;
        camaraPrincipal = GameObject.Find("Main Camera").GetComponent<Camera>();
        jugador = GameObject.Find("Jugador").transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        moverArma();
    }

    /// <summary>
    /// Controla que se pueda disparar o no
    /// </summary>
    /// <returns>si se puede disparar o no</returns>
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
        if (puedeDisparar)
        {
            puedeDisparar = false;
            GameObject nuevaBala;
            nuevaBala = Instantiate(bala, new Vector2(this.transform.position.x, this.transform.position.y) + (getDireccionDisparo() / getDireccionDisparo().magnitude) / 2, Quaternion.identity) as GameObject;
            nuevaBala.transform.LookAt(getMousePosition());
            nuevaBala.transform.parent = this.transform;
            StartCoroutine(calcularRatio(velocidadDisparo));
            Destroy(nuevaBala.transform.gameObject, 10);
        }
    }


}
