using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentanaEscena : MonoBehaviour
{
    public string escenaDestino; // Nombre de la escena de destino
    private bool jugadorEnVentana = false; // Indica si el jugador está en la puerta

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnVentana = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnVentana = false;
        }
    }

    void Update()
    {
        if (jugadorEnVentana && Input.GetKeyDown(KeyCode.Q))
        {
            CambiarEscena();
        }
    }

    void CambiarEscena()
    {
        if (!string.IsNullOrEmpty(escenaDestino))
        {
            SceneManager.LoadScene(escenaDestino);
        }
    }
}

