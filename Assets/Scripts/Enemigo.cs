using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class Enemigo : Personaje {

    // Daño que hace el enemigo sobre el personaje.
    public int danoAtaque = 3;

    private int vida;


    // Posición del jugador.
    private Transform objetivo;

	// Sobreescribimos la clase de start de la superclase.
	protected override void Start () {
        //objetivo = GameObject.FindGameObjectsWithTag("Jugador").transform;
        base.Start();
	}
	
    // Función que mueve el enemigo hacia la base del jugador o en caso que se encuentre en rango, mueve el enemigo hacia el jugador.
	public void moverEnemigo()
    {
        float x = 2;
        float y = 2;

        intentarMovimiento<Jugador>(x, y);

        // Pasamos como componente a Jugador, que es al que posiblemente nos encontremos.
        //intentarMovimiento<Muro>(x, y);
    }

    protected override void choqueConObjeto<T>(T componente)
    {
        // Comprobado si el enemigo se ha chocado contra el jugador.
        Jugador jugadorAtacado = componente as Jugador;

        //jugadorAtacado.quitarVida(danoAtaque);
    }

    
    // Comprueba que el jugador está en rango, si lo está perseguirle.
    private bool jugadorEnRango()
    {
        return true;
    }

    protected override void mover(Vector2 destino)
    {
        this.transform.LookAt(destino);
    }
}
