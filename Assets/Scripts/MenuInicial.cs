using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
   
    public void Jugar(string Escena1Nivel1)
    {
        SceneManager.LoadScene(Escena1Nivel1);
    }

   
    public void Salir()
    {
        Application.Quit();
    }
}
