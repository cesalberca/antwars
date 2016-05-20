using UnityEngine;
using System.Collections;

public class Proyectil : MonoBehaviour
{
    public int dano = 1;
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
        coll.transform.gameObject.GetComponent<Muro>().bajarVida(dano);
        Destroy(this.gameObject);
    }
}