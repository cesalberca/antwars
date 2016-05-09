using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Jugador : Personaje{

    public Arma armaSeleccionada;
    public List<Arma> almacenArmas = new List<Arma>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void elegirSiguienteArma()
    {
        armaSeleccionada = almacenArmas.Find(armaSeleccionada.id);
    }

    void elegirArma(Arma armaElegida)
    {
        armaSeleccionada = armaElegida;
    }

    protected override void choqueConObjeto<T>(T componente)
    {
        throw new NotImplementedException();
    }

    protected override void mover(Vector2 destino)
    {
        throw new NotImplementedException();
    }
}
