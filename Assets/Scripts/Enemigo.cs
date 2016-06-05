using UnityEngine;
using System.Collections;
using Pathfinding;

// Heredar de personaje
public class Enemigo : MonoBehaviour
{
    // Referencias a los objetos necesarios.
    public Transform objetivo;
    public Transform jugador;
    public Vector3 posicionObjetivo;
    public Base baseJugador;
    public Jugador jugadorReferencia;

    private Seeker seeker;
    private CharacterController controlador;

    // El camino a recorrer
    public Path path;

    // Campos propios del objeto
    public float velocidad = 1;
    public float rangoDeteccionEnemigo = 5;
    // La máxima distancia al próximo "punto de control."
    public float maximaDistanciaAtajo = 1;
    // El punto hacia el que nos movemos.
    private int puntoActual = 0;
    private int danioBase = 10;
    private int danioJugador = 10;


    public void Start()
    {
        posicionObjetivo = objetivo.transform.position;
        seeker = GetComponent<Seeker>();
        controlador = GetComponent<CharacterController>();

        seeker.StartPath(transform.position, posicionObjetivo, OnPathComplete);
    }

    public void Update()
    {
        posicionObjetivo = objetivo.transform.position;
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Reseteamos el index actual.
            puntoActual = 0;
        }
    }

    public void FixedUpdate()
    {
        // Comprobamos que hay un path hacia el objetivo. Si no es así, cavamos.
        if (path == null)
        {
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
            // Miramos al jugador.
            transform.LookAt(jugador.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            // Nos movemos en su dirección si la distancia con el jugador es mayor que uno, si no, le quitamos vida al jugador.
            if (Vector3.Distance(transform.position, jugador.position) > 1f)
            {
                transform.Translate(new Vector3(velocidad * Time.deltaTime, 0, 0));
            }
            else
            {
                jugadorReferencia.bajarVida(danioJugador);
                Destroy(this.gameObject);
            }
        } else
        {
            // Dirección al próximo punto
            Vector3 dir = (path.vectorPath[puntoActual] - transform.position).normalized;
            dir *= velocidad * Time.fixedDeltaTime;
            controlador.Move(dir);

            // Comprobamos que estamos cerca del próximo punto.
            if (Vector3.Distance(transform.position, path.vectorPath[puntoActual]) < maximaDistanciaAtajo)
            {
                puntoActual++;
                return;
            }
        }
    }

    /// <summary>
    /// Comprueba si el jugador está dentro de un radio determinado.
    /// </summary>
    /// <returns></returns>
    public bool dentroRadioJugador()
    {
        float radio = (transform.position - jugador.position).sqrMagnitude;
        if (radio < rangoDeteccionEnemigo)
        {
            return true;
        }

        return false;
    }
}
