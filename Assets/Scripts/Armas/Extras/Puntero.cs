using UnityEngine;
using System.Collections;

public class Puntero : MonoBehaviour {

    public Camera camaraPrincipal;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        seguirRaton();
	}

    void seguirRaton()
    {
        this.transform.position = new Vector3(getMousePosition().x, getMousePosition().y, -1);
    }

    private Vector3 getMousePosition()
    {
        Vector3 mousePos = new Vector2(camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).x, camaraPrincipal.ScreenToWorldPoint(Input.mousePosition).y);
        return mousePos;
    }
}
