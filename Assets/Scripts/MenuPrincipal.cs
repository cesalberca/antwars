using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    public Canvas canvasMenu;           //el canvas del menuPrincipal de eleccion
    public Canvas canvasNombres;        //el canvas con nuestros nombres
    public Canvas canvasPresentan;      //el canvas de "presentan"

	// Use this for initialization
	void Start () {
        //intro del juego, timed con la musica
        StartCoroutine(mostrarCanvas(canvasNombres, false, 20));
        StartCoroutine(mostrarCanvas(canvasPresentan, true, 20));
        StartCoroutine(mostrarCanvas(canvasPresentan, false, 33.5f));
        StartCoroutine(mostrarCanvas(canvasMenu, true, 33.5f));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// carga la nueva partida
    /// </summary>
    public void nuevaPartida()
    {
        SceneManager.LoadScene("juegoPrincipal");
    }

    /// <summary>
    /// se cierra el juego, si esta en modo editor, se para la simulacion
    /// </summary>
    public void cerrarJuego()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    /// <summary>
    /// controla el delay del canvas a mostrar
    /// </summary>
    /// <param name="canvas">el canvas que queremos manipular</param>
    /// <param name="estado">el estado que le queremos dar</param>
    /// <param name="delayTime">el delay que queremos que tenga la orden</param>
    /// <returns></returns>
    public IEnumerator mostrarCanvas(Canvas canvas, bool estado, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        canvas.transform.gameObject.SetActive(estado);
    }
}
