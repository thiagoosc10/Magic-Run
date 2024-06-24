using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    public float daño = 2f; // Daño que hace el disparo
    public GameObject orbePrefab; // Prefab de la orbe a generar
    public int cantidadOrbes = 1; // Cantidad de orbes a generar

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Bruja enemigo = other.gameObject.GetComponent<Bruja>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(daño);
            }
            Destroy(gameObject); // Destruir el disparo del jugador
        }

        if (other.gameObject.CompareTag("DisparoEnemigo"))
        {
            GenerarOrbes(other.gameObject.transform.position);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {         
            Destroy(gameObject);
        }
    }

    void GenerarOrbes(Vector2 posicion)
    {
        for (int i = 0; i < cantidadOrbes; i++)
        {
            Vector2 offset = Random.insideUnitCircle * 0.5f; // Genera una posición aleatoria en un radio de 0.5 unidades
            GameObject orbe = Instantiate(orbePrefab, posicion + offset, Quaternion.identity);
        }
    }
}


