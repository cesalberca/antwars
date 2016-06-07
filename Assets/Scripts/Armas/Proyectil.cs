using UnityEngine;
using System.Collections;

/// <summary>
/// el proyectil basico que disparan las armaPistola
/// </summary>
public class Proyectil : MonoBehaviour
{
    public int dano;        //el daño del proyectil

    /// <summary>
    /// controla que cuando se choque con un muro le baje la vida o si es con un enemigo le baje la vida
    /// </summary>
    /// <param name="coll">la collision que ha ocurrido</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.gameObject.GetComponent<Muro>())
        {
            coll.transform.gameObject.GetComponent<Muro>().bajarVida(dano);
            this.GetComponent<AudioSource>().PlayOneShot(this.GetComponent<AudioSource>().clip);
            Destroy(this.transform.gameObject);
        }
    }
}