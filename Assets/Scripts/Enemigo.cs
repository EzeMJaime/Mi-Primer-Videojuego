using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float vida = 3f;
    public float danio = 1f; // Daño que inflige al jugador
    public float distanciaMaximaDelJugador = 10f;
    public float distanciaParaAtacar = 1f; // Distancia para infligir daño
    private Transform jugador;
    private Jugador scriptJugador;

    void Start()
    {
        jugador = GameObject.Find("Jugador").transform;
        scriptJugador = jugador.GetComponent<Jugador>();

        float angulo = Random.Range(0f, 360f);
        float distancia = Random.Range(distanciaMaximaDelJugador - 5f, distanciaMaximaDelJugador);
        Vector2 posicion = new Vector2(
            jugador.position.x + Mathf.Cos(angulo) * distancia,
            jugador.position.y + Mathf.Sin(angulo) * distancia
        );
        transform.position = posicion;
    }

    void Update()
    {
        if (jugador == null) return;

        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);

        // Si el enemigo está cerca del jugador, inflige daño
        if (Vector2.Distance(transform.position, jugador.position) <= distanciaParaAtacar)
        {
            scriptJugador.RecibirDanio(danio * Time.deltaTime);
        }
    }

    public void RecibirDanio(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
