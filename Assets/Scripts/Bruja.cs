using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Bruja : MonoBehaviour
{
    public GameObject prefabDisparoEnemigo;
    public Transform puntoDeDisparo;
    public float tiempoMinEntreDisparos = 1f;
    public float tiempoMaxEntreDisparos = 6f;
    private float tiempoProximoDisparo;

    public float saludMaxima = 50f;
    public float salud;
    public UnityEvent<float> onSaludCambiar;

    public BarraVidaEnemigo barraVida;

    private SpriteRenderer spriteRenderer; // Añadimos esta línea

    void Start()
    {
        tiempoProximoDisparo = Time.time + Random.Range(tiempoMinEntreDisparos, tiempoMaxEntreDisparos);
        if (onSaludCambiar == null)
            onSaludCambiar = new UnityEvent<float>();
        
        salud = saludMaxima;
        ActualizarBarraVida();

        spriteRenderer = GetComponent<SpriteRenderer>(); // Inicializamos el SpriteRenderer
        if (spriteRenderer == null)
        {
            Debug.LogError("No se encontró el SpriteRenderer en la Bruja");
        }
    }

    void Update()
    {
        if (Time.time >= tiempoProximoDisparo)
        {
            Disparar();
            tiempoProximoDisparo = Time.time + Random.Range(tiempoMinEntreDisparos, tiempoMaxEntreDisparos);
        }
    }

    void Disparar()
    {
        GameObject disparo = Instantiate(prefabDisparoEnemigo, puntoDeDisparo.position, Quaternion.identity);
        disparo.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f);
    }

    public void RecibirDaño(float cantidad)
    {
        salud -= cantidad;
        ActualizarBarraVida();
        ActualizarColorBruja(); // Añadimos esta línea
        onSaludCambiar.Invoke(salud);
        if (salud <= 0)
        {
            Morir();
        }
    }

    void ActualizarBarraVida()
    {
        if (barraVida != null)
        {
            barraVida.ActualizarBarra(salud / saludMaxima);
        }
    }

    void ActualizarColorBruja() // Añadimos este método
    {
        if (spriteRenderer != null)
        {
            float porcentajeSalud = salud / saludMaxima;
            Color nuevoColor = Color.Lerp(Color.red, Color.white, porcentajeSalud);
            spriteRenderer.color = nuevoColor;
        }
    }

    void Morir()
    {
        if (barraVida != null)
        {
            Destroy(barraVida.gameObject);
        }

        SceneManager.LoadScene("Victoria");

        Destroy(gameObject);
    }

    public void AjustarDisparo(float saludActual)
    {
        if (saludActual <= 35f)
        {
            tiempoMinEntreDisparos *= 0.9f;
            tiempoMaxEntreDisparos *= 0.9f;
        }
    }
}




