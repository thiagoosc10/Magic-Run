using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver2 : MonoBehaviour
{
    // Método para cambiar de escena, se pasa el nombre de la escena como parámetro
    public void Reintentar(string Escena2Nivel1)
    {
        SceneManager.LoadScene(Escena2Nivel1);
    }

    // Método para salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}
