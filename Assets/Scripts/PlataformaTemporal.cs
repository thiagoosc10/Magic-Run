using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaTemporal : MonoBehaviour
{
    public float tiempoEspera;

    private Rigidbody2D rb2d;

    public float velocidadRotacion;

    private bool caida = false;
    private AudioSource audioSource;
    
    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

    }

    private void Update() 
    {
        if (caida)
        {
            transform.Rotate(new Vector3(0, 0, -velocidadRotacion * Time.deltaTime));
        } 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
           
            if (audioSource != null)
            {
                audioSource.Play();
            }

            StartCoroutine(Caida(other));
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator Caida(Collision2D other)
    {
        yield return new WaitForSeconds(tiempoEspera);
        caida = true;
        Physics2D.IgnoreCollision(transform.GetComponent<Collider2D>(), other.transform.GetComponent<Collider2D>());
        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.AddForce(new Vector2(0.1f, 0));
    }
}
