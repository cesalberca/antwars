using UnityEngine;
using System.Collections;

public class Muro : MonoBehaviour {
    public float posicionX; //Posición X del trozo de muro.
    public float posicionY; //Posición Y del trozo de muro.
    public int vidaMuro=5;   //Vida que tiene el trozo de muro.
    public int costConstruccion = 5;    //Coste de material para construirlo.

    public Muro (float _x , float _y)
    {
        this.posicionX = _x;
        this.posicionY = _y;
    }
    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// Este método comprueba si el trozo de muro aun existe por la vida que tiene. En caso de ser mayor de 0 devuelve true.
    /// </summary>
    /// <returns></returns>
    public bool checkVida ()
    {
        if (this.vidaMuro > 0)
        {
            return true;
        }
        else return false;
    }
    /// <summary>
    /// Este método baja la vida del muro dependiendo del parametro que le pasamos.
    /// </summary>
    /// <param name="danio"></param>
    public void bajarVida (int danio)
    {
        if (checkVida())
        {
            this.vidaMuro = this.vidaMuro - danio;
        }
        if(!checkVida())
        {
            Destroy(this.gameObject);
        }
    }
}
