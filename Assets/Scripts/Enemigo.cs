using UnityEngine;
using System.Collections;
using Pathfinding;

// Heredar de personaje
public class Enemigo : MonoBehaviour
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
    public float velocidad = 1;
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
        seeker = GetComponent<Seeker>();
        controlador = GetComponent<CharacterController>();
        posicionBase = GameObject.Find("base").transform;
        posicionJugador = GameObject.Find("Jugador").transform;
        baseJugador = GameObject.Find("base").GetComponent<Base>();
        jugador = GameObject.Find("Jugador").GetComponent<Jugador>();
        posicionObjetivo = posicionBase.transform.position;
        seeker.StartPath(transform.position, posicionObjetivo, OnPathComplete);
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
        } else
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

    void perseguirJugador()
    {
        transform.LookAt(posicionJugador.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        // Nos movemos en su dirección si la distancia con el jugador es mayor que uno, si no, le quitamos vida al jugador.
        transform.Translate(new Vector3(velocidad * Time.deltaTime, 0, 0));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(gameObject.name + "ha chocado con" + coll.gameObject.name);
        if (coll.gameObject.name == "Jugador")
        {
            jugador.bajarVida(danioJugador);
            Destroy(this.gameObject);
        } else
        {
            // Cavar
            Destroy(coll.gameObject);
        }
        

    }


    /// <summary>
    /// Comprueba si el jugador está dentro de un radio determinado.
    /// </summary>
    /// <returns></returns>
    public bool dentroRadioJugador()
    {
        float radio = (transform.position - posicionJugador.position).sqrMagnitude;
        if (radio < rangoDeteccionEnemigo)
        {
            return true;
        }

        return false;
    }
}
