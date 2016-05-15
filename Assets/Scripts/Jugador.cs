using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Jugador : Personaje{

    //cosas a pasar
    public Camera camaraPrincipal;                          //la camara principal que se quiere que siga al personaje
    public int distanciaInteraccion = 5;                    //la distancia a la que se pueden interactuar
    private int almacenMateriales = 100;                    //la cantidad de materiales que tiene el jugador
    public List<GameObject> almacenArmas;                   //la lista de armas que puede equipar

    //cosas a crear
    private GameObject armaSeleccionada;
    private int indexSeleccionado = 0;
    private ArmaBomba ab;


    // Use this for initialization
    void Start () {
        ab = armaSeleccionada.gameObject.GetComponent<ArmaBomba>();
	}
	
	// Update is called once per frame
	void Update () {
        mover(new Vector2());
        elegirArma();
        
    }

    /// <summary>
    /// elige un arma segun la tecla que pulses
    /// </summary>
    void elegirArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            elegirArmaPorIndex(0);
            aparecerArma();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            elegirArmaPorIndex(1);
            aparecerArma();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            elegirArmaPorIndex(2);
            aparecerArma();
        }
    }

    /// <summary>
    /// selecciona un arma segun el index que le pases
    /// </summary>
    /// <param name="index">La posicion del almacenArmas del arma que se quiere equipar</param>
    void elegirArmaPorIndex(int index)
    {
        armaSeleccionada = almacenArmas[index];
    }

    /// <summary>
    /// hace aparecer el arma seleccionada
    /// </summary>
    void aparecerArma()
    {
        matarHijos(this.gameObject);
        armaSeleccionada = Instantiate(armaSeleccionada, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity) as GameObject;
        armaSeleccionada.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// mata a todos los hijos del objeto pasado por parametro
    /// </summary>
    /// <param name="padre">el objeto del que queremos matar todos los hijos</param>
    void matarHijos(GameObject padre)
    {
        for (int i = 0; i< padre.transform.childCount; i++)
        {
            Destroy(padre.transform.GetChild(i).gameObject);
        }
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
            position.x -= velocidad;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x += velocidad;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.y += velocidad;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.y -= velocidad;
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
                StartCoroutine(ab.detonarBomba(nuevoCubo, delayBomba, radioBomba));
            }
        }
    }

    /// <summary>
    /// quita el impulso acumulado del objeto
    /// </summary>
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
