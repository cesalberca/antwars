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
        Destroy(this.transform.gameObject);
        Destroy(coll.transform.gameObject);
    }
}