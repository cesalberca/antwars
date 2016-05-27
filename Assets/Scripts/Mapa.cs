using UnityEngine;
using System.Collections;

public class Mapa : MonoBehaviour {
    public GameObject muro;
    public GameObject fondo;
    public int tilesFila = 5;                               //La altura del mundo generado
    public int tilesColumna = 5;                            //La anchura del mundo generado
    [Range(0, 100)]
    public int porcientoBloque = 75;        //El porciento de bloque/no bloque de la generacion de mundo
    GameObject[,] arrayTiles;


    // Use this for initialization
    void Start () {
        crearMundo();
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
            for (int j = 0; j < tilesColumna; j++)
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

        for (int j = 1; j < tilesColumna - 1; j++)
        {
            for (int destruidor = Random.Range(1, 5); destruidor < Random.Range(5, 10); destruidor++)
            {
                Destroy(arrayTiles[destruidor, j].transform.gameObject);
            }
        }
        /*for (int j = 1; j < tilesColumna - 1; j++)
        {
            for (int destruidor = Random.Range(10, 15); destruidor < Random.Range(15, 20); destruidor++)
            {
                Destroy(arrayTiles[destruidor, j].transform.gameObject);
            }
        }*/
        for (int j = 1; j < tilesColumna - 1; j++)
        {
            for (int destruidor = Random.Range(20, 25); destruidor < Random.Range(25, 29); destruidor++)
            {
                Destroy(arrayTiles[destruidor, j].transform.gameObject);
            }
        }

        for (int i = 1; i < tilesFila - 1; i++)
        {
            for (int destruidor = Random.Range(5, 10); destruidor < Random.Range(10, 15); destruidor++)
            {
                Destroy(arrayTiles[i, destruidor].transform.gameObject);
            }
        }
        for (int i = 1; i < tilesFila-1; i++)
        {
            for (int destruidor = Random.Range(15, 20); destruidor < Random.Range(20, 25); destruidor++)
            {
                Destroy(arrayTiles[i, destruidor].transform.gameObject);
            }

        }
    } 
    }
     
 
