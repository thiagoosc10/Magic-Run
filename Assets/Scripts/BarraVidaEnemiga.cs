using UnityEngine;
using UnityEngine.UI;

public class BarraVidaEnemigo : MonoBehaviour
{
    public Image barraImagen;
    public Transform enemigo;
    public Vector3 offset = new Vector3(0, 1f, 0);

    private void Update()
    {
        if (enemigo != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(enemigo.position + offset);
        }
        else
        {
            // Si el enemigo ya no existe, destruir la barra de vida
            Destroy(gameObject);
        }
    }

    public void ActualizarBarra(float porcentaje)
    {
        barraImagen.fillAmount = porcentaje;
    }
}
