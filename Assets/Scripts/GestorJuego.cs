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
        private List<Enemigo> enemigos;
		
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
            enemigos = new List<Enemigo>();
            iniciarJuego();
		}

        void iniciarJuego()
        {
            enemigos.Clear();
        }
		
		
		//Update is called every frame.
	}
}

