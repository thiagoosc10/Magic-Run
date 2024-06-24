using UnityEngine;

public class GeneradorDisparos : MonoBehaviour
{
    public float tiempoEntreApariciones = 10f;
    public float duracionAparicion = 5f;
    private float tiempoUltimaAparicion;
    private bool estaActivo = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Inicializar con un tiempo aleatorio para que no aparezcan todos a la vez
        tiempoUltimaAparicion = Time.time - Random.Range(0f, tiempoEntreApariciones);
        // Comenzar desactivado
        Desaparecer();
    }

    private void Update()
    {
        if (!estaActivo && Time.time - tiempoUltimaAparicion >= tiempoEntreApariciones)
        {
            Aparecer();
        }
        else if (estaActivo && Time.time - tiempoUltimaAparicion >= duracionAparicion)
        {
            Desaparecer();
        }
    }

    private void Aparecer()
    {
        estaActivo = true;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        tiempoUltimaAparicion = Time.time;
        if (animator != null)
        {
            animator.SetBool("isActive", true);
        }
    }

    private void Desaparecer()
    {
        estaActivo = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        if (animator != null)
        {
            animator.SetBool("isActive", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && estaActivo)
        {
            MovimientoJugador jugador = collision.GetComponent<MovimientoJugador>();
            if (jugador != null)
            {
                jugador.ObtenerDisparos(20);
                Desaparecer();
                // Reiniciar el temporizador para la próxima aparición
                tiempoUltimaAparicion = Time.time;
            }
        }
    }
}
