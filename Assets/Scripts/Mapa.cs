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
        void crearMundo() { 
    
            arrayTiles = new GameObject[tilesFila, tilesColumna];

            int elegidor;
            Muro muroACrear;



            for (int i = 0; i < tilesFila; i++)
            {
                for (int j = 0; j < tilesColumna; j++)
                {
                    muroACrear = new Muro(j, i);
                //if (arrayTiles[i, j-1].ge
                //{
                elegidor = Random.Range(0, 100);
                //}
                //else
                //{
                //    elegidor = Random.Range(0, 60);
                //}

                if (elegidor < porcientoBloque)
                    {
                        arrayTiles[i, j] = Instantiate(muro, new Vector3(muroACrear.posicionX, -muroACrear.posicionY, 0), Quaternion.identity) as GameObject;
                        arrayTiles[i, j].AddComponent<BoxCollider2D>();
                        arrayTiles[i, j].AddComponent<Muro>();
                        arrayTiles[i, j].GetComponent<Muro>().posicionX = muroACrear.posicionX;
                        arrayTiles[i, j].GetComponent<Muro>().posicionY = muroACrear.posicionY;
                        arrayTiles[i, j].GetComponent<Muro>().vidaMuro = muroACrear.vidaMuro;
                        arrayTiles[i, j].GetComponent<Muro>().costConstruccion = muroACrear.costConstruccion;
                        //arrayTiles[i, j].tag = "Default";
                    }
                    else { }
                    arrayTiles[i, j] = Instantiate(fondo, new Vector3(j, -i, 1), Quaternion.identity) as GameObject;//cambiar el resource load por una referencia
                }
            }
        }
    }
