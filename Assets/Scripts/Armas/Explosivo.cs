using UnityEngine;
using System.Collections;

public class Explosivo : MonoBehaviour
{
    public int delayBomba;
    public int radioBomba;
    public int danoBomba;
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
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(bomba.transform.position, radioBomba);
            //Instantiate(particulaBomba, bomba.transform.position, Quaternion.identity);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (!hitColliders[i].CompareTag("Jugador"))
                {
                    if (hitColliders[i].GetComponent<Muro>())
                    {
                        hitColliders[i].GetComponent<Muro>().bajarVida(danoBomba);
                    }
                    //Destroy(hitColliders[i].transform.gameObject);
                }
            }
        }
    }
}
