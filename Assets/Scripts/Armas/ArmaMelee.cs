using UnityEngine;
using System.Collections;

public class ArmaMelee : ArmaBasica {

	// Use this for initialization
	void Start () {
        camaraPrincipal = GameObject.Find("Main Camera").GetComponent<Camera>();
        jugador = GameObject.Find("Jugador").transform.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        moverArma();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.GetComponent<Personaje>())
        {
            coll.transform.GetComponent<Personaje>().bajarVida(this.dano);
        } else if (coll.transform.GetComponent<Muro>()){
            coll.transform.GetComponent<Muro>().bajarVida(this.dano);
        }
    }
}
