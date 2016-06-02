using UnityEngine;
using System.Collections;

public class Mapa : MonoBehaviour {
    public GameObject muro;
    public GameObject fondo;
    public GameObject centroOperaciones;
    public int tilesFila = 5;                               //La altura del mundo generado
    public int tilesColumna = 5;                            //La anchura del mundo generado
    GameObject[,] arrayTiles;
    [Range(1, 10)]
    public int porcientoCavernas = 5;
    [Range(0, 10)]
    public int tamañoBase = 5;

    // Use this for initialization
    void Start () {
        crearMundo();
        generaCavernas();
        huecoBase();
    }

    // Update is called once per frame
    void Update() {
    }


    void crearMundo()
    {
        arrayTiles = new GameObject[tilesFila, tilesColumna];

        Muro muroACrear;




        for (int i = 0; i < tilesFila; i++) //Con este doble For creamos el fondo del mapa;
        {
            for (int j = 0; j < tilesColumna; j++)
            {
                arrayTiles[i, j] = Instantiate(fondo, new Vector3(j, -i, 1), Quaternion.identity) as GameObject;//cambiar el resource load por una referencia
            }
        }

        for (int i = 0; i < tilesFila; i++)
        {
            for (int j = 0; j < tilesColumna; j++) //Aquí llenamos el mapa de muros.
            {
                muroACrear = new Muro(j, i);
                arrayTiles[i, j] = Instantiate(muro, new Vector3(muroACrear.posicionX, -muroACrear.posicionY, 0), Quaternion.identity) as GameObject;
                arrayTiles[i, j].AddComponent<BoxCollider2D>();
                arrayTiles[i, j].AddComponent<Muro>();
                arrayTiles[i, j].GetComponent<Muro>().posicionX = muroACrear.posicionX;
                arrayTiles[i, j].GetComponent<Muro>().posicionY = muroACrear.posicionY;
                arrayTiles[i, j].GetComponent<Muro>().vidaMuro = muroACrear.vidaMuro;
                arrayTiles[i, j].GetComponent<Muro>().costConstruccion = muroACrear.costConstruccion;
            }
        }

       }

    void generaCavernas() // Con este metodo y el siguiente se crean en el mapa huecos de forma aleatoria regulados por el valor 'porcientoCavernas'.
    {
        for (int a =0; a <= porcientoCavernas;a++)
            {
             for (int j = 0; j < tilesColumna; j++)
             {
                     int destruidor = Random.Range(0, tilesFila);
                     Destroy(arrayTiles[destruidor, j].transform.gameObject);
            }
                creaEspacios();
           }
    }


    void creaEspacios()
    {
        for (int j = 0; j < porcientoCavernas * 2; j++)
        {
            int posXBola = Random.Range(tilesFila / 8, tilesFila - (tilesFila / 8));
            int posYBola = Random.Range(tilesColumna / 8, tilesColumna - (tilesColumna / 8));
            for (int i = 0; i <= (tilesFila / 8) /2; i++)
            {
                Destroy(arrayTiles[posXBola + i, posYBola + i].transform.gameObject);
                Destroy(arrayTiles[posXBola, posYBola + i].transform.gameObject);
                Destroy(arrayTiles[posXBola + i, posYBola].transform.gameObject);
                Destroy(arrayTiles[posXBola, posYBola - i].transform.gameObject);
                Destroy(arrayTiles[posXBola - i, posYBola].transform.gameObject);
                Destroy(arrayTiles[posXBola - i, posYBola - i].transform.gameObject);
                Destroy(arrayTiles[posXBola - i, posYBola + i].transform.gameObject);
                Destroy(arrayTiles[posXBola + i, posYBola - i].transform.gameObject);
            }
        }
    }

    void huecoBase() //Aquí creamos en el centro del mapa un especio donde se ubicará la base que defenderemos del enemigo. Se controla su tamaño con la variable 'tamañoBase'.
    {
        int posXBase = (tilesColumna / 2) - (tamañoBase / 2);
        int posYBase = (tilesFila / 2) - (tamañoBase / 2);

        for (int i = 0; i < tamañoBase; i++)
        {
            for ( int j = 0; j < tamañoBase; j++ )
            {
                Destroy(arrayTiles[(posXBase+i),(posYBase+j)].transform.gameObject);
            }
        }

        centroOperaciones = Instantiate(centroOperaciones, new Vector3(posXBase+tamañoBase/2, -posYBase - tamañoBase / 2, 0), Quaternion.identity) as GameObject;
        centroOperaciones.AddComponent<BoxCollider2D>();
        //centroOperaciones.AddComponent<Muro>();

    }
    }
     
 
