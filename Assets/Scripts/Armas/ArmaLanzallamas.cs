using UnityEngine;
using System.Collections;

public class ArmaLanzallamas : ArmaBasica
{

    private Quaternion lookRotation;

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
            disparar();
        }
        moverArma();
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
            //nuevaBala.AddComponent<BoxCollider2D>();
            //nuevaBala.AddComponent<Rigidbody2D>();
            //nuevaBala.GetComponent<Rigidbody2D>().gravityScale = 0;

            nuevaBala.transform.LookAt(getMousePosition());
            nuevaBala.transform.parent = this.transform;
            //nuevaBala.transform.localScale = new Vector3(1, 1, 1);
            StartCoroutine(calcularRatio(velocidadDisparo));
            Destroy(nuevaBala.transform.gameObject, 10);
        }
    }


}
