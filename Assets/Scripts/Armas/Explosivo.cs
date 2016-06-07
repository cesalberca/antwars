using UnityEngine;
using System.Collections;

/// <summary>
/// Contiene funciones que convierten al gameObject al que tenga el explosivo como atributo a una bomba
/// </summary>
public class Explosivo : ArmaBasica
{
    public int delayBomba;              //el delay que tarda la bomba en explotar
    public int radioBomba;              //el radio de la explosion en la que la bomba tendra efecto
    public int danoBomba;               //el daño de la bomba 
    public GameObject explosion;        //el efecto de la explosion

    void disparar() {
    }

    /// <summary>
    /// Hace daño a todos los objetos en el area pasada por parametro
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

    /// <summary>
    /// si la bomba se choca con algo despues de haber sido colocada, explota
    /// </summary>
    /// <param name="coll">la collision</param>
    public void OnCollisionEnter2D(Collision2D coll)
    {
        StartCoroutine(detonarBomba(this.transform.gameObject, 0, radioBomba));
    }
}
