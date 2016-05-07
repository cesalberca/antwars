using UnityEngine;
using System.Collections;
using System;

public class Enemigo : Personaje {

    // Daño que hace el enemigo sobre el personaje.
    public int danoAtaque = 3;

    // Posición del jugador.
    private Transform objetivo;

	// Sobreescribimos la clase de start de la superclase.
	protected override void Start () {
        //objetivo = GameObject.FindGameObjectsWithTag("Jugador").transform;
        base.Start();
	}
	
	public void moverEnemigo()
    {
        int x = 1;
        int y = 1;

        intentarMovimiento<Jugador>(x, y);
    }

    protected override void choqueConObjeto<T>(T componente)
    {
        // Comprobado si el enemigo se ha chocado contra el jugador.
        Jugador jugadorAtacado = componente as Jugador;

        //jugadorAtacado.quitarVida(danoAtaque);

    }

    protected override void mover(Vector2 destino)
    {
        throw new NotImplementedException();
    }
}
