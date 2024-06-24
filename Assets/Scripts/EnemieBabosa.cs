using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 2f; // Velocidad de movimiento del enemigo
    public float tiempoCambioDireccion = 5f; // Tiempo en segundos para cambiar de dirección
    private bool moviendoDerecha = true; // Indica si el enemigo se está moviendo hacia la derecha
    private Rigidbody2D rb;
    private float tiempoRestante;
    private Animator animator;
    private bool isDead = false;
    public GameObject orbePrefab;
    public int cantidadOrbes = 3;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tiempoRestante = tiempoCambioDireccion;
        animator = GetComponent<Animator>(); // Asegúrate de que el enemigo tenga un componente Animator
    }

    void Update()
    {
        if (isDead) return; // Si el enemigo está muerto, no realizar ningún movimiento

        // Actualizar la dirección y velocidad del enemigo
        if (moviendoDerecha)
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
        }

        // Reducir el tiempo restante
        tiempoRestante -= Time.deltaTime;

        // Cambiar de dirección cuando el tiempo se agota
        if (tiempoRestante <= 0)
        {
            moviendoDerecha = !moviendoDerecha;
            tiempoRestante = tiempoCambioDireccion;

            // Voltear la escala del enemigo para reflejar el cambio de dirección
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
    
    public void Die()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        // Generar orbes
        for (int i = 0; i < cantidadOrbes; i++)
        {
            Vector2 posicionAleatoria = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
            Instantiate(orbePrefab, posicionAleatoria, Quaternion.identity);
        }

        Destroy(gameObject, 0.2f);
    }
}

