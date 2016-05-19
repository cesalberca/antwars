using UnityEngine;
using System.Collections;

public class tempCreadorMundo : MonoBehaviour
{

    public GameObject muro;
    public GameObject fondo;
    public int tilesFila = 5;                               //La altura del mundo generado
    public int tilesColumna = 5;                            //La anchura del mundo generado
    [Range(0, 100)]
    public int porcientoBloque = 75;        //El porciento de bloque/no bloque de la generacion de mundo
    GameObject[,] arrayTiles;                               //El array de objetos que forman el mundo

    // Use this for initialization
    void Start()
    {
        crearMundo();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Crea el mundo
    /// </summary>
    void crearMundo()
    {
        arrayTiles = new GameObject[tilesFila, tilesColumna];

        int elegidor;

        for (int i = 0; i < tilesFila; i++)
        {
            for (int j = 0; j < tilesColumna; j++)
            {
                elegidor = Random.Range(0, 100);
                if (elegidor < porcientoBloque)
                {
                    arrayTiles[i, j] = Instantiate(muro, new Vector3(j, -i, 0), Quaternion.identity) as GameObject;//cambiar el resource load por una referencia
                    arrayTiles[i, j].AddComponent<BoxCollider2D>();
                    //arrayTiles[i, j].tag = "Default";
                }
                else { }
                arrayTiles[i, j] = Instantiate(fondo, new Vector3(j, -i, 1), Quaternion.identity) as GameObject;//cambiar el resource load por una referencia
            }
        }
    }
}