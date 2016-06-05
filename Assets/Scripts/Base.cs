using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    private float posicionX;
    private float posicionY;
    public int vida = 100;

	// Use this for initialization
	void Start () {
        this.posicionX = 200;
        this.posicionY = 200;	
	}

    public float PosicionX
    {
        get { return posicionX; }
        set { posicionX = value; }
    }

    public float PosicionY
    {
        get { return posicionY; }
        set { posicionY = value; }
    }

    public void bajarVida(int danio)
    {
        if (vida > 0)
        {
            this.vida = this.vida - danio;
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
