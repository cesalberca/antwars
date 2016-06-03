using UnityEngine;
using System.Collections;
using System;
using Pathfinding;

public class Enemigo : Personaje {

    // Elemento que se perseguirá
    public Transform target;
    // Posición del objetvio
    public Vector3 targetPosition;
    // A* para que busque su objetivo
    Seeker seeker;
    // Trayecto generado por el gráfico de A*.
    Path path;
    // Index actual.
    private int indexActual = 0;
    public float distanciaAlProximoPuntoDeControl = 3;

    public int danoAtaque = 3; // Daño que hace el enemigo sobre el personaje.
    

    private int vida; //Vida que tiene el enemigo.
    private Rigidbody rb;


	// Sobreescribimos la clase de start de la superclase.
	protected override void Start () {
        // Conseguimos la referencia de la posición del jugador.
        targetPosition = target.transform.position;
        rb = GetComponent<Rigidbody>();
        base.Start();
	}

    protected void Update()
    {
        // Cada frame se buscará la posición del objetivo.
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, PathCompletado);
    }

    public void PathCompletado(Path p)
    {
        if (!p.error)
        {
            path = p;
            // Resetamos el index actual.
            indexActual = 0;
        }
    }

    public void FixedUpate()
    {
        if (path == null)
        {
            // Si no tenemos path salimos.
            return;
        }

        if (indexActual >= path.vectorPath.Count)
        {
            // Fin del trayecto.
            return;
        }

        // Movimiento hacia el objetivo.
        Vector3 dir = (path.vectorPath[indexActual] - transform.position).normalized;
        dir *= velocidad * Time.fixedDeltaTime;
        rb.AddForce(dir); 

        // Si estamos cerca del próximo elemento del path procedeemos a buscar un nuevo index.
        if (Vector3.Distance(transform.position, path.vectorPath[indexActual]) < distanciaAlProximoPuntoDeControl)
        {
            indexActual++;
            return;
        }

    }

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
