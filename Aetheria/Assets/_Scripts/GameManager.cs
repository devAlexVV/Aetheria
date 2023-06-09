using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMannager : MonoBehaviour
{
    //Maneja el flujo del juego
    /*
     * Ejemplo :
    El menu de pausa.
    El cambio entre niveles
    La pantalla de muerte
    Interaciones con el mundo segun el estado del jugador.
          -Algo asi como si el mae esta bajo de vida ahora puede encontrar mas salud.
       
     */
    public static GameMannager Instance;
    void Awake()
    {
        //Esto esta creando una instancia "estatica" manualmente, solo para poder accesar
        //este script y su info desde multiples lugares
        Instance = this; 
    }

}

//Ahorita no hay muchos estados, tonces dejo solo el dead :v
public enum GameState { 
    Dead
}
