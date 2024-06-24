using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbe : MonoBehaviour
{
    public float velocidad = 5f;
    public int valorPuntos = 10;
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (jugador != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí llamaremos a una función para sumar puntos al jugador
            other.GetComponent<MovimientoJugador>().SumarPuntos(valorPuntos);
            Destroy(gameObject);
        }
    }
}
