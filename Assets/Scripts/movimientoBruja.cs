using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBruja : MonoBehaviour
{
    public float velocidad = 2f; // Velocidad de movimiento de la bruja
    private bool moviendoDerecha = true; // Indica si la bruja se está moviendo hacia la derecha
    private Rigidbody2D rb;
    private Bruja bruja; // Cambiado de Enemigo a Bruja

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bruja = GetComponent<Bruja>(); // Cambiado de Enemigo a Bruja

        if (bruja != null)
        {
            bruja.onSaludCambiar.AddListener(AjustarVelocidad);
            bruja.onSaludCambiar.AddListener(bruja.AjustarDisparo); // Asegúrate de que la función AjustarDisparo esté suscrita al evento
        }
    }

    // El resto del código permanece igual

    void Update()
    {
        // Actualizar la dirección y velocidad de la bruja
        if (moviendoDerecha)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftBoundary") || collision.gameObject.CompareTag("RightBoundary"))
        {
            moviendoDerecha = !moviendoDerecha;
            Voltear();
        }
    }

    void Voltear()
    {
        // Voltear la escala de la bruja para reflejar el cambio de dirección
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    void AjustarVelocidad(float saludActual)
    {
        if (saludActual <= 25f) // Por ejemplo, aumentar la velocidad cuando la salud es menor o igual a 25
        {
            velocidad *= 1.2f; 
        }
    }
}


