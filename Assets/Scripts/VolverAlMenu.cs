using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlMenu : MonoBehaviour
{
    public void CargarMenuInicial()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void Salir()
    {
        Application.Quit();
    }
}

