using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5;
    public float velocidadSaltoInicial = 2f;
    public float velocidadSaltoMaxima = 5f;
    public float tiempoSaltoMaximo = 0.5f; // Tiempo máximo que se puede mantener el salto

    private Rigidbody2D rb; // Componente Rigidbody2D del jugador
    private Animator animator;
    private bool haSaltado = false; // Indica si el jugador ha realizado un salto en el aire
    private float worldWidth;
    private bool saltando = false;
    public float tiempoSaltando = 0f; // Tiempo que se ha mantenido presionada la tecla de salto
    public GameObject prefabDisparo; // Referencia al prefab del disparo
    public Transform puntoDeDisparo;   // Punto desde donde se disparará el disparo
    public float velocidadDisparoJugador = 10f; // Velocidad del disparo
    private int disparosDisponibles = 0;
    public int maximoDisparos = 20;
    public Image barraDisparos; // Referencia a la imagen de la barra de disparos
    private int puntos = 0;
    public Text textoPuntos; // Referencia al componente Text de la UI
    public float duracionAnimacion = 0.25f;
    public float escalaMaxima = 1.2f;
    private Vector3 escalaOriginal;


    void Start()
    {
        ActualizarTextoPuntos();
        // Obtener la referencia al componente Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Obtener el tamaño horizontal del mundo (pantalla)
        worldWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        if (barraDisparos != null)
        {
            barraDisparos.gameObject.SetActive(false);
        }

        if (textoPuntos != null)
        {
            escalaOriginal = textoPuntos.transform.localScale;
        }
        ActualizarTextoPuntos();
    }

    public void OnFire()
    {
        if (disparosDisponibles > 0)
        {
            Disparar();
            disparosDisponibles--;
            ActualizarBarraDisparos();
        }
    }

    void Update()
    {
        // Obtener la entrada del eje horizontal (izquierda/derecha)
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        if (movimientoHorizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (movimientoHorizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        animator.SetBool("running", movimientoHorizontal != 0.0f);

        // Calcular el vector de movimiento
        Vector2 movimiento = new Vector2(movimientoHorizontal * velocidad, rb.velocity.y);

        // Aplicar el movimiento al Rigidbody del jugador
        rb.velocity = movimiento;

        // Verificar si el jugador ha traspasado un borde y reposicionarlo
        if (transform.position.x > worldWidth)
        {
            // Reposicionar al jugador en el borde opuesto
            transform.position = new Vector3(-worldWidth, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -worldWidth)
        {
            // Reposicionar al jugador en el borde opuesto
            transform.position = new Vector3(worldWidth, transform.position.y, transform.position.z);
        }

        // Verificar si se ha presionado la tecla de salto
        if (Input.GetKeyDown(KeyCode.Space) && CheckSuelo.enSuelo && !haSaltado)
        {
            saltando = true;
            tiempoSaltando = 0f;
            rb.velocity = new Vector2(rb.velocity.x, velocidadSaltoInicial);
            haSaltado = true;
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }

        // Continuar aplicando fuerza de salto mientras se mantenga presionada la tecla y no se supere el tiempo máximo de salto
        if (Input.GetKey(KeyCode.Space) && saltando && tiempoSaltando < tiempoSaltoMaximo)
        {
            tiempoSaltando += Time.deltaTime;
            float factorSalto = Mathf.Lerp(velocidadSaltoInicial, velocidadSaltoMaxima, tiempoSaltando / tiempoSaltoMaximo);
            rb.velocity = new Vector2(rb.velocity.x, factorSalto);
        }

        // Detener el salto cuando se suelte la tecla o se supere el tiempo máximo de salto
        if (Input.GetKeyUp(KeyCode.Space) || tiempoSaltando >= tiempoSaltoMaximo)
        {
            saltando = false;

         if (rb.velocity.y < 0 && !CheckSuelo.enSuelo)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }
        else if (CheckSuelo.enSuelo)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }

        }
    }

    private void FixedUpdate()
    {
        if (CheckSuelo.enSuelo)
        {
            haSaltado = false;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el jugador colisiona con la cabeza del enemigo
        if (collision.gameObject.CompareTag("ObjectHeadEnemy"))
        {
            Destroy(collision.transform.parent.gameObject); // Destruir al enemigo (el padre de este objeto)
            rb.velocity = new Vector2(rb.velocity.x, velocidadSaltoInicial); // Realizar un salto después de aplastar al enemigo
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador colisiona con el enemigo
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Obtener la posición del jugador y del enemigo
            Vector2 JugadorPos = transform.position;
            Vector2 enemigoPos = collision.transform.position;

            // Verificar si el punto de contacto está por encima del centro del enemigo
            foreach (ContactPoint2D contacto in collision.contacts)
            {
                if (contacto.point.y > enemigoPos.y + collision.collider.bounds.size.y / 2)
                {
                    MovimientoEnemigo enemigo = collision.gameObject.GetComponent<MovimientoEnemigo>();
                    if (enemigo != null)
                    {
                        enemigo.Die(); // Matar al enemigo
                        rb.velocity = new Vector2(rb.velocity.x, velocidadSaltoInicial); // Realizar un salto después de aplastar al enemigo
                    }
                    return; // Salir del método si el enemigo ha sido aplastado
                }
                
            }

            // Si no se aplastó al enemigo, destruir al jugador
            Destroy(gameObject);
            Morir();
        }
        // Verificar si el jugador colisiona con el enemigo
        if (collision.gameObject.CompareTag("Muerte"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver Escena 2");
        }
    }

    void Disparar()
    {
        // Verificar si el prefabDisparo no es null antes de instanciarlo
        if (prefabDisparo != null)
        {
            GameObject proyectil = Instantiate(prefabDisparo, puntoDeDisparo.position, Quaternion.identity);
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.up * velocidadDisparoJugador; 
            }
        }
        else
        {
            Debug.LogError("El prefab del disparo no está asignado.");
        }
    }

      public void ObtenerDisparos(int cantidad)
    {
        disparosDisponibles = Mathf.Min(disparosDisponibles + cantidad, maximoDisparos);
        ActualizarBarraDisparos();
    }

    // Método para cambiar a la escena de GameOver
    void Morir()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void ActualizarBarraDisparos()
    {
        if (barraDisparos != null)
        {
            if (disparosDisponibles > 0)
            {
                barraDisparos.gameObject.SetActive(true);
                barraDisparos.fillAmount = (float)disparosDisponibles / maximoDisparos;
            }
            else
            {
                barraDisparos.gameObject.SetActive(false);
            }
        }
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarTextoPuntos();
        StartCoroutine(AnimarTextoPuntos());
    }

    private void ActualizarTextoPuntos()
    {
        if (textoPuntos != null)
        {
            textoPuntos.text = "" + puntos;
        }
    }

    private IEnumerator AnimarTextoPuntos()
    {
        if (textoPuntos != null)
        {
            // Agrandar
            float tiempoPasado = 0f;
            while (tiempoPasado < duracionAnimacion / 2)
            {
                tiempoPasado += Time.deltaTime;
                float t = tiempoPasado / (duracionAnimacion / 2);
                textoPuntos.transform.localScale = Vector3.Lerp(escalaOriginal, escalaOriginal * escalaMaxima, t);
                yield return null;
            }

            // Volver al tamaño original
            tiempoPasado = 0f;
            while (tiempoPasado < duracionAnimacion / 2)
            {
                tiempoPasado += Time.deltaTime;
                float t = tiempoPasado / (duracionAnimacion / 2);
                textoPuntos.transform.localScale = Vector3.Lerp(escalaOriginal * escalaMaxima, escalaOriginal, t);
                yield return null;
            }

            textoPuntos.transform.localScale = escalaOriginal;
        }
    }
}

