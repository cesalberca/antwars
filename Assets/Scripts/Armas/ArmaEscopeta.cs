using UnityEngine;
using System.Collections;

public class ArmaEscopeta : ArmaBasica
{

    // Use this for initialization
    void Start()
    {
        puedeDisparar = true;
        camaraPrincipal = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        jugador = this.transform.parent.gameObject;
        if (Input.GetKey(KeyCode.F))
        {
            if (puedeDisparar)
            {
                
                disparar((new Vector2(getDireccionDisparo().x + 0, getDireccionDisparo().y + 0) / getDireccionDisparo().magnitude) * potencia);
                
            }
        }
        moverArma();
    }

    /// <summary>
    /// dispara el proyectil de este arma cada vez que el calcularRatio, la tasa de fuego, le deja
    /// </summary>
    void disparar(Vector2 vectorDisparo)
    {
        puedeDisparar = false;
        GameObject nuevaBala;
        nuevaBala = Instantiate(bala, new Vector2(this.transform.position.x, this.transform.position.y) + (getDireccionDisparo() / getDireccionDisparo().magnitude) / 10, Quaternion.identity) as GameObject;
        nuevaBala.AddComponent<BoxCollider2D>();
        nuevaBala.AddComponent<Rigidbody2D>();
        nuevaBala.GetComponent<Rigidbody2D>().gravityScale = 0;
        nuevaBala.GetComponent<Rigidbody2D>().AddRelativeForce(vectorDisparo);
        //nuevaBala.tag = "BloqueConstruido";
        nuevaBala.GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
        //nuevaBala.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(calcularRatio(velocidadDisparo));
        Destroy(nuevaBala, 10);
    }
}
