using UnityEngine;
using System.Collections;
using Pathfinding;

// Heredar de personaje
public class Enemigo : MonoBehaviour
{

    public Transform objetivo;
    public Vector3 posicionObjetivo;

    private Seeker seeker;
    private CharacterController controlador;

    // El camino a recorrer
    public Path path;

    public float velocidad = 1;

    // La máxima distancia al próximo "punto de control."
    public float maximaDistanciaAtajo = 1;

    // El punto hacia el que nos movemos.
    private int puntoActual = 0;


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
        if (path == null)
        {
            return;
        }

        if (puntoActual >= path.vectorPath.Count)
        {
            return;
        }

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
