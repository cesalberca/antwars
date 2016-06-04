using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    public Canvas canvasMenu;
    public Canvas canvasNombres;
    public Canvas canvasPresentan;
    public Scene escena;

	// Use this for initialization
	void Start () {
        StartCoroutine(mostrarCanvas(canvasNombres, false, 20));
        StartCoroutine(mostrarCanvas(canvasPresentan, true, 20));
        StartCoroutine(mostrarCanvas(canvasPresentan, false, 33.5f));
        StartCoroutine(mostrarCanvas(canvasMenu, true, 33.5f));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void nuevaPartida()
    {
        SceneManager.LoadScene("juegoPrincipal");
    }

    public void cerrarJuego()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public IEnumerator mostrarCanvas(Canvas canvas, bool estado, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canvas.transform.gameObject.SetActive(estado);
    }
}
