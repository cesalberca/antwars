using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// el jugador que controlamos en el juego
/// </summary>
public class Jugador : Personaje
{

    //cosas a pasar
    public Camera camaraPrincipal;                          //la camara principal que se quiere que siga al personaje
    public int distanciaInteraccion = 5;                    //la distancia a la que se pueden interactuar
    public int almacenMateriales = 100;                     //la cantidad de materiales que tiene el jugador
    public float velocidadPicar = 0.5f;                     //la velocidad que tarda el jugador en poder picar
    public List<GameObject> almacenArmas;                   //la lista de armas que puede equipar
    public List<GameObject> almacenMuros;                   //la lista de todos los tipos de muros que puede colocar
    public GameObject HUD;                                  //el Heads Up Display del jugador

    //cosas a crear
    private GameObject armaSeleccionada;                    //el arma seleccionada actualmente
    private int indexSeleccionado = 0;                      //el indice del arma seleccionada
    private bool puedePicar = true;                         //variable controladora que posibilita poder picar o no


    // Use this for initialization
    void Start()
    {
        HUD.GetComponent<GestorHUD>().refrescar();
    }

    // Update is called once per frame
    void Update()
    {
        mover(new Vector2());
        elegirArma();
        cambiarArma();
        resetImpulso();
        controlarMirada();
        picar();
        construir();
        controlarBombas();
        disparar();
        AstarPath.active.Scan();
    }

    #region INVENTARIO

    /// <summary>
    /// Cambia de arma con la rueda del raton
    /// </summary>
    void cambiarArma()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (indexSeleccionado > 0)
            {
                elegirArmaPorIndex(indexSeleccionado - 1);
                aparecerArma();
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (indexSeleccionado < almacenArmas.Count - 1)
            {
                elegirArmaPorIndex(indexSeleccionado + 1);
                aparecerArma();
            }
        }
    }

    /// <summary>
    /// controla el disparar del jugador, si tiene suficientes materiales dispara y le quita los materiales necesarios
    /// </summary>
    void disparar()
    {
        if (Input.GetMouseButton(0) && this.almacenMateriales >= armaSeleccionada.GetComponent<ArmaBasica>().gastoDisparo)
        {
            if (armaSeleccionada.GetComponent<ArmaBasica>().puedeDisparar)
            {
                armaSeleccionada.SendMessage("controlarDisparo");
                this.almacenMateriales = this.almacenMateriales - armaSeleccionada.GetComponent<ArmaBasica>().gastoDisparo;
                HUD.GetComponent<GestorHUD>().refrescar();
            }
        }
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
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            elegirArmaPorIndex(3);
            aparecerArma();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            elegirArmaPorIndex(4);
            aparecerArma();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            elegirArmaPorIndex(5);
            aparecerArma();
        }
    }

    /// <summary>
    /// Elige el armaseleccionada segun el boton pulsado en el HUD
    /// </summary>
    /// <param name="indexArma">el indice del arma que se quiere elegir</param>
    public void elegirArmaBoton(int indexArma)
    {
        elegirArmaPorIndex(indexArma);
        aparecerArma();
    }

    /// <summary>
    /// selecciona un arma segun el index que le pases
    /// </summary>
    /// <param name="index">La posicion del almacenArmas del arma que se quiere equipar</param>
    void elegirArmaPorIndex(int index)
    {
        armaSeleccionada = almacenArmas[index];
        indexSeleccionado = index;
    }

    /// <summary>
    /// hace aparecer el arma seleccionada
    /// </summary>
    void aparecerArma()
    {
        matarHijos(this.gameObject);
        armaSeleccionada = Instantiate(armaSeleccionada, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity) as GameObject;
        armaSeleccionada.transform.parent = this.transform;
    }

    /// <summary>
    /// mata a todos los hijos del objeto pasado por parametro
    /// </summary>
    /// <param name="padre">el objeto del que queremos matar todos los hijos</param>
    void matarHijos(GameObject padre)
    {
        for (int i = 0; i < padre.transform.childCount; i++)
        {
            Destroy(padre.transform.GetChild(i).gameObject);
        }
    }
    #endregion

    #region MOVIMIENTO
    /// <summary>
    /// mueve al jugador
    /// </summary>
    /// <param name="destino">La posicion a la que se quiere ir</param>
    protected override void mover(Vector2 destino)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.x -= velocidad;
            //transform.localRotation = Quaternion.Euler(0, 180, 0);
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x += velocidad;
            // transform.localRotation = Quaternion.Euler(0, 0, 0);
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
    /// modo secundario de movimiento, no implementado
    /// </summary>
    protected void mover2()
    {
        float movex;
        float movey;
        float velocidad = 100;
        if (Input.GetKey(KeyCode.A))
        {
            movex = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movex = 1;
        }
        else
        {
            movex = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            movey = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movey = -1;
        }
        else
        {
            movey = 0;
        }
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(movex * velocidad, movey * velocidad);
    }

    /// <summary>
    /// Controla que el jugador siempre mire a la posicion del raton
    /// </summary>
    public void controlarMirada()
    {
        if (getMousePosition().x > this.transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    #endregion

    #region LOGICA
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
    /// Recoge la posicion del raton en el mundo y la devuelve
    /// </summary>
    /// <returns>la posicion del raton en el mundo</returns>
    private Vector3 getMousePosition()
    {
        Vector3 mousePos = new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePos;
    }

    /// <summary>
    /// quita el impulso acumulado del objeto
    /// </summary>
    void resetImpulso()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    /// <summary>
    /// controla del delay de picar
    /// </summary>
    /// <param name="delayTime"></param>
    /// <returns></returns>
    public IEnumerator delayPicar(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        puedePicar = true;
    }
    #endregion

    #region FEATURES

    /// <summary>
    /// controla el picar del jugador
    /// </summary>
    void picar()
    {
        if (Input.GetMouseButton(1))
        {
            controlarDestruccion();
        }
    }

    /// <summary>
    /// controla el construir del jugador
    /// </summary>
    void construir()
    {
        if (Input.GetMouseButtonDown(2))
        {
            controlarConstruccion(almacenMuros[0]);
        }
    }

    /// <summary>
    /// destruye el objeto seleccionado por seleccionarGameObjectMouse
    /// </summary>
    void controlarDestruccion()
    {
        if (puedePicar)
        {
            if (getDistanciaMouseObject(this.gameObject) < distanciaInteraccion)
            {
                if (seleccionarGameObjectMouse() != null)
                {
                    //if (!seleccionarGameObjectMouse().CompareTag("Jugador") && !seleccionarGameObjectMouse().CompareTag("Enemigo"))
                    if (!seleccionarGameObjectMouse().CompareTag("Jugador") && seleccionarGameObjectMouse().GetComponent<Muro>())
                    {
                        puedePicar = false;
                        seleccionarGameObjectMouse().GetComponent<Muro>().bajarVida(1);
                        this.almacenMateriales = this.almacenMateriales + 1;
                        HUD.GetComponent<GestorHUD>().refrescar();
                        StartCoroutine(delayPicar(velocidadPicar));
                    }
                }
            }
        }
    }

    /// <summary>
    /// contruye un objeto en la posicion del raton, si no hay otro objeto en su posicion
    /// </summary>
    /// <param name="bloqueAConstruir">el gameobject que se va a colocar</param>
    /// <returns> el gameObject creado</returns>
    GameObject controlarConstruccion(GameObject bloqueAConstruir)
    {
        if (getDistanciaMouseObject(this.gameObject) < distanciaInteraccion)
        {
            if (seleccionarGameObjectMouse() == null && this.almacenMateriales >= bloqueAConstruir.GetComponent<Muro>().costConstruccion)
            {
                GameObject nuevoCubo;//cambiar el resource load por una referencia
                nuevoCubo = Instantiate(bloqueAConstruir, new Vector3(Mathf.Round(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y), 0), Quaternion.identity) as GameObject;
                //nuevoCubo.AddComponent<Muro>();
                //nuevoCubo.tag = "BloqueConstruido";
                nuevoCubo.AddComponent<BoxCollider2D>();
                nuevoCubo.layer = 9;
                this.almacenMateriales = this.almacenMateriales - nuevoCubo.GetComponent<Muro>().costConstruccion;
                HUD.GetComponent<GestorHUD>().refrescar();
                AstarPath.active.Scan();
                return nuevoCubo;
            }
            else return null;
        }
        else return null;

    }

    /// <summary>
    /// controla el colocar bombas del jugador
    /// </summary>
    void controlarBombas()
    {
        if (Input.GetMouseButton(0) && this.almacenMateriales >= armaSeleccionada.GetComponent<ArmaBasica>().gastoDisparo)
        {
            if (armaSeleccionada.GetComponent<Explosivo>())
            {
                colocarBomba(armaSeleccionada.GetComponent<Explosivo>().delayBomba, armaSeleccionada.GetComponent<Explosivo>().radioBomba);
            }
        }
    }

    /// <summary>
    /// crea un objeto bomba y llama a detonarBomba()
    /// </summary>
    void colocarBomba(int delayBomba, int radioBomba)
    {
        Explosivo nuevaBomba = armaSeleccionada.GetComponent<Explosivo>();
        this.almacenMateriales = this.almacenMateriales - armaSeleccionada.GetComponent<ArmaBasica>().gastoDisparo;
        StartCoroutine(nuevaBomba.detonarBomba(controlarConstruccion(armaSeleccionada), delayBomba, radioBomba));
    }

    protected override void onCollisionEnter(Collision2D coll)
    {
        throw new NotImplementedException();
    }
    #endregion
}
