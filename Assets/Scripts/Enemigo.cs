using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;
using Pathfinding;

public class Enemigo : Personaje {

    public Transform target;
    Seeker seeker;
    Path path;
    int currentIndex;
    float speed = 10;


    public int danoAtaque = 3; // Daño que hace el enemigo sobre el personaje.

    private int vida; //Vida que tiene el enemigo.


    // Posición del jugador.
    private Transform objetivo;

	// Sobreescribimos la clase de start de la superclase.
	protected override void Start () {
        //objetivo = GameObject.FindGameObjectsWithTag("Jugador").transform;
        vida = Random.Range(100, 150);
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        base.Start();
	}

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentIndex = 0;
        }
    }

    void FixedUpate()
    {
        if (path == null)
        {
            return;
        }

        if (currentIndex >= path.vectorPath.Count)
        {
            return;
        }

        transform.position = path.vectorPath[currentIndex];
        currentIndex++;
    }

    // Función que mueve el enemigo hacia la base del jugador o en caso que se encuentre en rango, mueve el enemigo hacia el jugador.
    //public void moverEnemigo(Base baseJugador)
    //   {
    //       float x = baseJugador.PosicionX;
    //       float y = baseJugador.PosicionY;

    //       // Pasamos como componente a Jugador, que es al que posiblemente nos encontremos.
    //       //intentarMovimiento<Muro>(x, y);
    //   }

    //protected override void choqueConObjeto<T>(T componente)
    //{
    //    // Comprobado si el enemigo se ha chocado contra el jugador.
    //    Jugador jugadorAtacado = componente as Jugador;

    //    //jugadorAtacado.quitarVida(danoAtaque);
    //}

    // Comprueba que el jugador está en rango, si lo está perseguirle.
    private bool jugadorEnRango()
    {
        return true;
    }

    protected override void mover(Vector2 destino)
    {
        throw new NotImplementedException();
    }

    protected override void onCollisionEnter(Collision2D coll)
    {
        throw new NotImplementedException();
    }
}
