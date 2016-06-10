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
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 5);
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

    // Función que mueve al personaje.
    protected abstract void mover();
}
