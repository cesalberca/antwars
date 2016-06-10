using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

// Heredar de personaje
public class Enemigo : Personaje
{
    // Referencias a los objetos necesarios.
    public Transform posicionBase;
    public Transform posicionJugador;
    public Vector3 posicionObjetivo;
    public Base baseJugador;
    public Jugador jugador;

    private Seeker seeker;
    private CharacterController controlador;

    // El camino a recorrer
    public Path path;
    // Campos propios del objeto
    
    public float rangoDeteccionEnemigo = 5;
    // La máxima distancia al próximo "punto de control."
    public float maximaDistanciaAtajo = 1;
    // El punto hacia el que nos movemos.
    private int puntoActual = 0;
    private int danioBase = 10;
    private int danioJugador = 10;
    private bool haSeguidoJugador;


    public void Start()
    {
        this.velocidad = 1;

        // Conseguimos los componentes necesarios
        seeker = GetComponent<Seeker>();
        controlador = GetComponent<CharacterController>();
        posicionBase = GameObject.Find("base(Clone)").transform;
        posicionJugador = GameObject.Find("Jugador").transform;
        baseJugador = GameObject.Find("base(Clone)").GetComponent<Base>();
        jugador = GameObject.Find("Jugador").GetComponent<Jugador>();
        // Determinamos la posicion de la base
        posicionObjetivo = posicionBase.transform.position;
        // Inicializamos la búsqueda de recorrido
        seeker.StartPath(transform.position, posicionObjetivo, OnPathComplete);
    }

    public void FixedUpdate()
    {
        mover();
    }

    /// <summary>
    /// Función que se ejecuta al completar el path por A*
    /// </summary>
    /// <param name="p">Path que se ha completado</param>
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Reseteamos el index actual.
            puntoActual = 0;
        }
    }

    /// <summary>
    /// Función que hace al enemigo persiguir al jugador
    /// </summary>
    void perseguirJugador()
    {
        transform.LookAt(posicionJugador.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        // Nos movemos en su dirección si la distancia con el jugador es mayor que uno, si no, le quitamos vida al jugador.
        transform.Translate(new Vector3(velocidad * Time.deltaTime, 0, 0));
    }

    /// <summary>
    /// Función que determina las colisiones con otros objetos que cuenten con un rigidbody
    /// </summary>
    /// <param name="coll">Elemento con el que se colisiona</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Jugador")
        {
            jugador.bajarVida(danioJugador);
            Destroy(this.gameObject);
        } else if (coll.gameObject.name == "MunicionPistola(Clone)")
        {
            bajarVida(20);
            Destroy(coll.gameObject);
        } else if (coll.gameObject.name == "base(Clone)")
        {
            baseJugador.bajarVida(danioBase);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Función que determina si el enemigo está actualmente en contacto con un objeto.
    /// </summary>
    /// <param name="coll">Elemento con el que se colisiona</param>
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.name == "spriteNormal(Clone)")
        {
            coll.gameObject.GetComponent<Muro>().bajarVida(1);
        }
    }


    /// <summary>
    /// Comprueba si el jugador está dentro de un radio determinado.
    /// </summary>
    /// <returns>Jugador dentro del radio o no</returns>
    public bool dentroRadioJugador()
    {
        float radio = (transform.position - posicionJugador.position).sqrMagnitude;
        if (radio < rangoDeteccionEnemigo)
        {
            return true;
        }

        return false;
    }

    protected override void mover()
    {
        // Comprobamos que hay un path hacia el objetivo. Si no es así, cavamos.
        if (path == null)
        {
            transform.LookAt(posicionBase.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
            return;
        }

        // Comprobamos si ha alcanzado el objetivo.
        if (puntoActual >= path.vectorPath.Count)
        {
            baseJugador.bajarVida(danioBase);
            Destroy(this.gameObject);
            return;
        }

        // Si estamos cerca del jugador seguirle a él.
        if (dentroRadioJugador())
        {
            perseguirJugador();
            haSeguidoJugador = true;
        }
        else
        {
            // Si ha seguido al jugador en algún momento, recalculamos la ruta.
            if (haSeguidoJugador)
            {
                seeker.StartPath(transform.position, posicionObjetivo, OnPathComplete);
                haSeguidoJugador = false;
                return;
            }

            // Dirección al próximo punto
            Vector3 dir = (path.vectorPath[puntoActual] - transform.position).normalized;
            dir *= velocidad * Time.fixedDeltaTime;
            this.transform.Translate(dir);

            // Comprobamos que estamos cerca del próximo punto.
            if (Vector3.Distance(transform.position, path.vectorPath[puntoActual]) < maximaDistanciaAtajo)
            {
                puntoActual++;
                return;
            }
        }
    }
}
