using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puerta : MonoBehaviour
{
    public Puerta puertaDestino; // Referencia a la puerta de destino
    private bool jugadorEnPuerta = false; // Indica si el jugador está en la puerta

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnPuerta = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnPuerta = false;
        }
    }

    void Update()
    {
        if (jugadorEnPuerta && Input.GetKeyDown(KeyCode.Q))
        {
            TeletransportarJugador();
        }
    }

    void TeletransportarJugador()
    {
        if (puertaDestino != null)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Player");
            jugador.transform.position = puertaDestino.transform.position;
        }
    }
}

