using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    private float posicionX;
    private float posicionY;

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
}
