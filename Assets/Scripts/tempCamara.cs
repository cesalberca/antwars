using UnityEngine;
using System.Collections;

public class tempCamara : MonoBehaviour {

    public float dampTime = 0.15f;                          //El tiempo que tarda la camara en reaccionar al movimiento del personaje
    private Vector3 velocidad = Vector3.zero;               //La velocidad de seguimiento
    public Transform objetivo;                              //La posicion del objeto que se quiere seguir

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        camara();
	}

    /// <summary>
    /// Controla la camara y sigue al objetivo
    /// </summary>
    void camara()
    {
        if (objetivo)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(objetivo.position);
            Vector3 delta = objetivo.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocidad, dampTime);
        }
    }
}
