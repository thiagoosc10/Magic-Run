using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovilSimple : MonoBehaviour
{
    public float distanciaMovimiento = 5f; // Distancia que la plataforma se moverá hacia arriba y hacia abajo
    public float velocidad = 2f; // Velocidad de movimiento de la plataforma
    public float intervaloMovimiento = 2f; // Tiempo en segundos entre cada cambio de dirección

    private Vector3 posicionInicial;
    private Vector3 posicionDestino;
    private float tiempoProximoCambio;
    private bool moviendoArriba = true;

    void Start()
    {
        posicionInicial = transform.position;
        posicionDestino = new Vector3(transform.position.x, transform.position.y + distanciaMovimiento, transform.position.z);
        tiempoProximoCambio = Time.time + intervaloMovimiento;
    }

    void Update()
    {
        // Mueve la plataforma hacia la posición destino
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        // Cambia la dirección de movimiento cuando se alcanza el intervalo de tiempo
        if (Time.time >= tiempoProximoCambio)
        {
            if (moviendoArriba)
            {
                posicionDestino = new Vector3(transform.position.x, posicionInicial.y + distanciaMovimiento, transform.position.z);
            }
            else
            {
                posicionDestino = posicionInicial;
            }

            // Alterna el valor de moviendoArriba
            moviendoArriba = !moviendoArriba;

            // Establece el tiempo para el próximo cambio de dirección
            tiempoProximoCambio = Time.time + intervaloMovimiento;
        }
    }
}
