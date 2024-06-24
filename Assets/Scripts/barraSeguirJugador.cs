using UnityEngine;

public class barraSeguirJugador : MonoBehaviour
{
    public Transform Jugador;
    public Vector3 offset = new Vector3(0, 1.5f, 0); // Ajusta esto para posicionar la barra

    void Update()
    {
        if (Jugador != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(Jugador.position + offset);
        }
    }
}