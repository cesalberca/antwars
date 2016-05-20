using UnityEngine;
using System.Collections;

public class Proyectil : MonoBehaviour
{

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
        coll.transform.gameObject.GetComponent<Muro>().vidaMuro--;
        if (coll.transform.gameObject.GetComponent<Muro>().vidaMuro <= 0)
        {
            Destroy(coll.transform.gameObject);
        }
        Destroy(this.transform.gameObject);
        //if (!coll.transform.CompareTag("Jugador"))
        //{
        //    Destroy(this.transform.gameObject);
        //    Destroy(coll.transform.gameObject);
        //}
    }
}