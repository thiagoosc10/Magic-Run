using UnityEngine;
using UnityEngine.SceneManagement;

public class DisparoEnemigo : MonoBehaviour
{
    public float velocidadRotacion = 180f; // Grados por segundo
    private Rigidbody2D rb;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();
    velocidadRotacion = Random.Range(velocidadRotacion * 0.8f, velocidadRotacion * 1.2f);
}

    void Update()
    {
        // Rotar el objeto
        transform.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);         
            Destroy(gameObject);
            Morir();
        }

        if (other.CompareTag("Ground"))
        {         
            Destroy(gameObject);
        }
    }

    void Morir()
    {
        SceneManager.LoadScene("GameOver Escena 2");
    }
}



