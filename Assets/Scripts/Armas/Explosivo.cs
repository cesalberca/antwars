using UnityEngine;
using System.Collections;

public class Explosivo : ArmaBasica
{
    public int delayBomba;
    public int radioBomba;
    public int danoBomba;
    //public bool esBomba;    
    public GameObject explosion;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void disparar() {
        //Explosivo nuevaBomba = this.GetComponent<Explosivo>();
        //colocarBomba(nuevaBomba.delayBomba, nuevaBomba.radioBomba);
    }

    /// <summary>
    /// Destruye todos los objetos en el area pasada por parametro
    /// </summary>
    /// <param name="bomba">El GameObject bomba creado anteriormente</param>
    /// <param name="delayTime">El tiempo de delay entre la llamada de la funcion y la detonacion de la bomba</param>
    /// <returns></returns>
    public IEnumerator detonarBomba(GameObject bomba, float delayTime, int radioBomba)
    {
        yield return new WaitForSeconds(delayTime);
        if (bomba != null)
        {
            bomba.GetComponent<AudioSource>().PlayOneShot(bomba.GetComponent<AudioSource>().clip);
            explosion = Instantiate(explosion, bomba.transform.position, Quaternion.identity) as GameObject;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(bomba.transform.position, radioBomba);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].GetComponent<Personaje>())
                {
                    hitColliders[i].GetComponent<Personaje>().bajarVida(danoBomba);
                } else if (hitColliders[i].GetComponent<Muro>())
                {
                    hitColliders[i].GetComponent<Muro>().bajarVida(danoBomba);
                }
            }
            Destroy(bomba.transform.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("ha colisionado");
        this.detonarBomba();
    }
}
