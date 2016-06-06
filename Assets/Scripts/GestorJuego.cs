using UnityEngine;
using System.Collections;

namespace Completed
{
	using System.Collections.Generic;		//Allows us to use Lists. 
	using UnityEngine.UI;					//Allows us to use UI.
	
	public class GestorJuego : MonoBehaviour
	{

        // Instancia estático del GestorJuego para poder accederla desde otras clases.
        public static GestorJuego instance = null;
        // Array de enemigos
        public List<GameObject> enemigos;
        private int puntuacion = 20;
		
		void Awake()
		{
            // Este if comprueba si hay una instancia de GestorJuego, si la hay la destruye para evitar que se instancia una nueva.
            if (instance == null)
            {
                instance = this;
            } else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            //enemigos = new List<GameObject>();
            iniciarJuego();
		}

        void iniciarJuego()
        {
            //enemigos.Clear();
            //int numeroEnemigos = (int)Mathf.Log(puntuacion, 2f);
            int numeroEnemigos = 10;

            for (int i = 0; i < numeroEnemigos; i++)
            {
                Debug.Log("hola");
                //enemigos.Add();
                Instantiate(enemigos[0], new Vector3 (i*5, -5*i, 0), Quaternion.identity);
            }
           
        }
    }
}

