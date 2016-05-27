using UnityEngine;
using System.Collections;

public abstract class Personaje : MonoBehaviour {

    // Velocidad
    public float velocidad = 0.1f;
    // Vida
    public int vida = 100;

    public void bajarVida(int danio)
    {
        if (estaVivo())
        {
            this.vida = this.vida - danio;
        }
        if (!estaVivo())
        {
            Destroy(this.gameObject);
        }
    }

    public bool estaVivo()
    {
        if (this.vida > 0)
        {
            return true;
        }
        else return false;
    }

    // Es protected y virtual para que se pueda sobreescribir por sus hijas.
    protected virtual void Start () {
    }

    // Función que mueve al personaje.
    protected abstract void mover(Vector2 destino);

    protected abstract void onCollisionEnter(Collision2D coll);

}
