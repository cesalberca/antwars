using UnityEngine;
using System.Collections;

public class Proyectil : MonoBehaviour
{
    public int dano = 1;
    //public AudioClip choque;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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