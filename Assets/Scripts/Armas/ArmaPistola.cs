using UnityEngine;
using System.Collections;

public class ArmaPistola : ArmaBasica {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void disparar()
    {
        GameObject nuevaBala;
        nuevaBala = Instantiate(bala, new Vector2(this.transform.position.x, this.transform.position.y) + getDireccionDisparo() / getDireccionDisparo().magnitude, Quaternion.identity) as GameObject;
        nuevaBala.AddComponent<BoxCollider2D>();
        nuevaBala.AddComponent<Rigidbody2D>();
        nuevaBala.GetComponent<Rigidbody2D>().gravityScale = 0;
        nuevaBala.GetComponent<Rigidbody2D>().AddRelativeForce((getDireccionDisparo() / getDireccionDisparo().magnitude) * velocidadDisparo);
        nuevaBala.tag = "BloqueConstruido";
        nuevaBala.GetComponent<BoxCollider2D>().size = new Vector2(0.3f, 0.3f);
        nuevaBala.transform.localScale = new Vector3(1, 1, 1);
    }
}
