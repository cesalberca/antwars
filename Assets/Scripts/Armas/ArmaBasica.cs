using UnityEngine;
using System.Collections;

public class ArmaBasica : MonoBehaviour {

    //public int id;
    public int dano;
    public int potencia;
    //public int amplitud;
    public int gastoDisparo;
    public float velocidadDisparo;
    public Camera camaraPrincipal;
    public GameObject bala;
    public GameObject jugador;

    //como se hará para que algo sea publico pero no se pase por parametro?
    public bool puedeDisparar;

    void Start()
    {
        //Debug.Log("HOLA");
        //camaraPrincipal = GameObject.Find("Main Camera").GetComponent<Camera>();
        jugador = this.transform.parent.gameObject;
    }
    /// <summary>
    /// cambiar para que sus hijos tengas distintos tipos de disparo
    /// </summary>

    /// <summary>
    /// 
    /// </summary>
    /// <returns>la posicion del raton en el mundo</returns>
    public Vector3 getMousePosition()
    {
        Vector3 mousePos = new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePos;
    }

    /// <summary>
    /// 
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
        this.transform.position = new Vector2(jugador.transform.position.x, jugador.transform.position.y) + (getDireccionDisparo() / getDireccionDisparo().magnitude)/3;
        rotarArma();
    }

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
