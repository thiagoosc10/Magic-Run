using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirJugador : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador
    public float offsetY = 0f; // Desplazamiento vertical opcional de la cámara con respecto al jugador

    void LateUpdate()
    {
        // Verificar si el jugador existe
        if (jugador != null)
        {
            // Obtener la posición actual del jugador y aplicar el desplazamiento vertical opcional
            Vector3 targetPosition = new Vector3(transform.position.x, jugador.position.y + offsetY, transform.position.z);

            // Actualizar la posición de la cámara para seguir al jugador verticalmente
            transform.position = targetPosition;
        }
    }
}


