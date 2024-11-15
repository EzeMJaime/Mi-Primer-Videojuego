using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float vida = 3f;
    public float distanciaMaximaDelJugador = 10f; // Radio para el spawn
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.Find("Jugador").transform;
        // Posicionar al enemigo a una distancia alejada del jugador
        float angulo = Random.Range(0f, 360f);
        float distancia = Random.Range(distanciaMaximaDelJugador - 5f, distanciaMaximaDelJugador); // Rango aleatorio dentro del radio
        Vector2 posicion = new Vector2(
            jugador.position.x + Mathf.Cos(angulo) * distancia,
            jugador.position.y + Mathf.Sin(angulo) * distancia
        );
        transform.position = posicion;
    }

    void Update()
    {
        // Mover al enemigo hacia el jugador
        if (jugador != null)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
        }
    }

    // Método para recibir daño
    public void RecibirDanio(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // Aquí puedes agregar una animación o efectos antes de destruir el objeto
        Destroy(gameObject); // Eliminar el enemigo
    }
}
