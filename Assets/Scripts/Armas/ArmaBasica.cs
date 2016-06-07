using UnityEngine;
using System.Collections;

/// <summary>
/// El arma basica del juego, todas las armas heredan de ella
/// </summary>
public class ArmaBasica : MonoBehaviour
{

    public int dano;                    //el daño que hace el arma
    public int potencia;                //la potencia con la que sale la bala del arma
    public int gastoDisparo;            //el gasto de materiales de disparar el arma
    public float velocidadDisparo;      //la velocidad con la que puede disparar el arma
    public Camera camaraPrincipal;      //la camara principal
    public GameObject bala;             //la bala que el arma dispara
    public GameObject jugador;          //el jugador que empuña el arma

    public bool puedeDisparar;          //controla que el arma o no pueda disparar

    void Start()
    {
        jugador = this.transform.parent.gameObject;
    }

    /// <summary>
    /// captura y devuelve la posicion del raton en el mundo
    /// </summary>
    /// <returns>la posicion del raton en el mundo</returns>
    public Vector3 getMousePosition()
    {
        Vector3 mousePos = new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePos;
    }

    /// <summary>
    /// Calcula un vector entre el arma y la posicion del raton
    /// </summary>
    /// <returns>el vector entre el raton y el jugador</returns>
    public Vector2 getDireccionDisparo()
    {
        Vector2 direccion = getMousePosition() - jugador.transform.position;
        return direccion;
    }

    /// <summary>
    /// mueve el arma segun la posicon del raton
    /// </summary>
    public void moverArma()
    {
        this.transform.position = new Vector2(jugador.transform.position.x, jugador.transform.position.y) + (getDireccionDisparo() / getDireccionDisparo().magnitude) / 3;
        rotarArma();
    }

    /// <summary>
    /// rota el arma para que apunte donde esta el puntero
    /// </summary>
    public void rotarArma()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    /// <summary>
    /// permite disparar el arma segun cuando se ha disparado por ultima vez y su tasa de fuego
    /// </summary>
    /// <param name="delayTime">la tasa de fuego del arma, cada cuanto puede disparar</param>
    /// <returns></returns>
    public IEnumerator calcularRatio(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        puedeDisparar = true;
    }

}
