using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSuelo : MonoBehaviour
{
    public static bool enSuelo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enSuelo = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enSuelo = false;
    }
}
