using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckSuelo"))
        {
            Destroy(transform.parent.gameObject); // Destruir al enemigo (el padre de este objeto)
        }
    }
}
