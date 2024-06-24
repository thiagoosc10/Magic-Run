using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    // Método para cambiar de escena, se pasa el nombre de la escena como parámetro
    public void Jugar(string MenuInicial)
    {
        SceneManager.LoadScene(MenuInicial);
    }

    // Método para salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}
