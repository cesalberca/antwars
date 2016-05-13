using UnityEngine;
using System.Collections;

public abstract class Personaje : MonoBehaviour {

    // Velocidad
    public float velocidad = 0.1f;
    // Vida
    public int vida = 100;

    // Capa donde se comprobará la colisión.
    public LayerMask capa;

    private BoxCollider2D colisionador;
    public Rigidbody2D cuerpo;

	// Es protected y virtual para que se pueda sobreescribir por sus hijas.
	protected virtual void Start () {
        colisionador = GetComponent<BoxCollider2D>();
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Función que determina si el personaje se puede mover en esa dirección.
    protected bool puedeMover(float x, float y, out RaycastHit2D choque)
    {
        // Cogemos la posición del objeto
        Vector2 comienzo = transform.position;
        // Determinamos la posición a la que se va a mover el objeto.
        Vector2 destino = comienzo + new Vector2(x, y);
        // Desactivamos el colisionador para evitar que detecte una colisión consigo mismo.
        colisionador.enabled = false;
        // Hacemos uso del RayCast para castear una linea hacia la dirección propuesta, se guardará el resultado en la variable choque.
        choque = Physics2D.Linecast(comienzo, destino, capa);
        // Volvemos a activar el colisionador.
        colisionador.enabled = true;

        // Determinamos si ha habido algún choque con algún objeto.
        if (choque.transform == null)
        {
            // moverse
            mover(destino);
            return true;
        }
        return false;
    }

    // Función que mueve al personaje.
    protected abstract void mover(Vector2 destino);

    // Función que intenta moverse en cierta dirección. Se llamará cada vez que el usuario presione una tecla y cada frame por las hormigas enemigas.
    protected virtual void intentarMovimiento <T> (float x, float y)
        where T : Component
    {
        RaycastHit2D choque;
        bool puedeMoverse = puedeMover(x, y, out choque);

        // Comprueba que nada se ha chocado con el linecast de puedeMover().
        if (choque.transform == null)
        {
            // Si no se ha chocado con nada no se ejecuta más código.
            return;
        }

        // Guarda el componente con el que el linecast se ha chocado.
        T componenteChoque = choque.transform.GetComponent<T>();

        // Si no se puede mover y se ha chocado con algo con lo que puede interactuar ejecuta una función.
        if (!puedeMoverse && componenteChoque != null)
        {
            // Cavar
            choqueConObjeto(componenteChoque);
        }
    }

    // Función que determina la lógica a seguir en caso que no se pueda mover en esa dirección.
    protected abstract void choqueConObjeto<T>(T componente)
            where T : Component;
}
