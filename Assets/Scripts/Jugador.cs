using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Jugador : Personaje{

    //cosas a pasar
    public Camera camaraPrincipal;                          //la camara principal que se quiere que siga al personaje
    public int distanciaInteraccion = 5;                    //la distancia a la que se pueden interactuar
    private int almacenMateriales = 100;                    //la cantidad de materiales que tiene el jugador

    //cosas a crear
    private Arma armaSeleccionada;
    private List<Arma> almacenArmas = new List<Arma>();
    

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void elegirArmaPorIndex(int index)
    {
        armaSeleccionada = almacenArmas[index];
    }

    void elegirArma(Arma armaElegida)
    {
        armaSeleccionada = armaElegida;
    }
    
    /// <summary>
    /// modo cutre para mover, deberiamos cambiarlo
    /// </summary>
    /// <param name="destino"></param>
    protected override void mover(Vector2 destino)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.x -= 0.05f;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x += 0.05f;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.y += 0.05f;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.y -= 0.05f;
            this.transform.position = position;
        }
    }

    /// <summary>
    /// Devuelve el objeto debajo de la posicion del raton
    /// </summary>
    /// <returns>El objeto debajo de la posicion del raton</returns>
    GameObject seleccionarGameObjectMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f);
        if (hit)
        {
            return hit.transform.gameObject;
        }
        else return null;
    }

    /// <summary>
    /// devuelve la distancia entre el personaje y el GameObject pasado como parametro
    /// </summary>
    /// <param name="jugador"></param>
    /// <returns>La distancia entre el personaje y el GameObject</returns>
    float getDistanciaMouseObject(GameObject objeto)
    {
        float distancia = Vector2.Distance(new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y), new Vector2(objeto.transform.position.x, objeto.transform.position.y));
        return distancia;
    }

    /// <summary>
    /// destruye el objeto seleccionado por seleccionarGameObjectMouse
    /// </summary>
    void controlarDestruccion()
    {
        if (getDistanciaMouseObject(this.gameObject) < distanciaInteraccion)
        {
            if (seleccionarGameObjectMouse() != null)
            {
                if (!seleccionarGameObjectMouse().CompareTag("Player") && !seleccionarGameObjectMouse().CompareTag("Enemigo"))
                {
                    Destroy(seleccionarGameObjectMouse());
                }
            }
        }
        
    }

    /// <summary>
    /// contruye un objeto en la posicion del raton, si no hay otro objeto en su posicion
    /// </summary>
    void controlarConstruccion(GameObject bloqueAConstruir)
    {
        if (getDistanciaMouseObject(this.gameObject) < distanciaInteraccion)
        {
            if (seleccionarGameObjectMouse() == null)
            {
                GameObject nuevoCubo;//cambiar el resource load por una referencia
                nuevoCubo = Instantiate(bloqueAConstruir, new Vector3(Mathf.Round(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y), 0), Quaternion.identity) as GameObject;
                nuevoCubo.AddComponent<Muro>();
                nuevoCubo.tag = "BloqueConstruido";
                nuevoCubo.AddComponent<BoxCollider2D>();
            }
        }
        
    }

    /// <summary>
    /// crea un objeto bomba y llama a detonarBomba()
    /// </summary>
    void colocarBomba(int delayBomba, int radioBomba)
    {
        if (getDistanciaMouseObject(this.gameObject) < distanciaInteraccion)
        {
            if (seleccionarGameObjectMouse() == null)
            {
                GameObject nuevoCubo;//cambiar el resource load por una referencia
                nuevoCubo = Instantiate(Resources.Load("Prefabs/spriteBomba"), new Vector3(Mathf.Round(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y), 0), Quaternion.identity) as GameObject;
                nuevoCubo.tag = "BloqueConstruido";
                nuevoCubo.AddComponent<BoxCollider2D>();
                StartCoroutine(detonarBomba(nuevoCubo, delayBomba, radioBomba));
            }
            
        }
    }

    /// <summary>
    /// Destruye todos los objetos en el area pasada por parametro
    /// </summary>
    /// <param name="bomba">El GameObject bomba creado anteriormente</param>
    /// <param name="delayTime">El tiempo de delay entre la llamada de la funcion y la detonacion de la bomba</param>
    /// <returns></returns>
    IEnumerator detonarBomba(GameObject bomba, float delayTime, int radioBomba)
    {
        yield return new WaitForSeconds(delayTime);
        if (bomba != null)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(bomba.transform.position, radioBomba);
            //Instantiate(particulaBomba, bomba.transform.position, Quaternion.identity);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (!hitColliders[i].CompareTag("Player"))
                {
                    Destroy(hitColliders[i].transform.gameObject);
                }
            }
        }
    }

    void resetImpulso()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    protected override void choqueConObjeto<T>(T componente)
    {
        throw new NotImplementedException();
    }
}
