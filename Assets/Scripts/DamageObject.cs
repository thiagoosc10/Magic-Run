using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DamageObject : MonoBehaviour
{

   private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión fue con un objeto en la capa "Player"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Destroy(collision.gameObject);
            Morir();
        }
    }

    void Morir()
    {
        SceneManager.LoadScene("GameOver");
    }
}

