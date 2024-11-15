using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float vida = 10f; // Vida máxima del jugador
    public float danio = 1f; // Daño del jugador al atacar
    public float alcanceDeAtaque = 2f; // Distancia desde el jugador para el ataque
    public float anguloDeAtaque = 60f; // Ángulo del triángulo de ataque
    public LayerMask capaEnemigos; // Capa de los enemigos
    public float tiempoCooldown = 1f; // Cooldown entre ataques
    private bool enCooldown = false; // Indica si el jugador está en cooldown
    public GameObject efectoTrianguloPrefab; // Prefab para el diseño visual del ataque

    private bool estaMuerto = false; // Indica si el jugador está muerto

    void Update()
    {
        if (estaMuerto) return;

        if (!enCooldown)
        {
            // Detectar enemigos dentro del rango del triángulo de ataque
            Collider2D[] enemigosEnRango = DetectarEnemigosEnRango();

            if (enemigosEnRango.Length > 0)
            {
                Atacar(enemigosEnRango); // Realiza el ataque
                StartCoroutine(ActivarCooldown()); // Inicia el cooldown
                MostrarEfectosVisuales(enemigosEnRango); // Muestra los efectos visuales del ataque
            }
        }
    }

    // Detectar enemigos dentro de un rango triangular
    Collider2D[] DetectarEnemigosEnRango()
    {
        return Physics2D.OverlapCircleAll(transform.position, alcanceDeAtaque, capaEnemigos);
    }

    // Realizar el ataque sobre los enemigos detectados
    void Atacar(Collider2D[] enemigos)
    {
        foreach (var enemigo in enemigos)
        {
            enemigo.GetComponent<Enemigo>().RecibirDanio(danio);
        }
    }

    // Mostrar efectos visuales del ataque
    void MostrarEfectosVisuales(Collider2D[] enemigos)
    {
        if (efectoTrianguloPrefab == null) return;

        Transform enemigoMasCercano = EncontrarEnemigoMasCercano(enemigos);
        if (enemigoMasCercano != null)
        {
            Vector3 direccionAtaque = (enemigoMasCercano.position - transform.position).normalized;
            float anguloRotacion = Mathf.Atan2(direccionAtaque.y, direccionAtaque.x) * Mathf.Rad2Deg;
            Vector3 posicionEfecto = transform.position + direccionAtaque * alcanceDeAtaque;

            GameObject efecto = Instantiate(efectoTrianguloPrefab, posicionEfecto, Quaternion.Euler(0, 0, anguloRotacion));
            Destroy(efecto, 0.5f);
        }
    }

    // Encontrar al enemigo más cercano
    Transform EncontrarEnemigoMasCercano(Collider2D[] enemigos)
    {
        Transform enemigoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (var enemigo in enemigos)
        {
            float distancia = Vector2.Distance(transform.position, enemigo.transform.position);
            if (distancia < distanciaMinima)
            {
                enemigoMasCercano = enemigo.transform;
                distanciaMinima = distancia;
            }
        }

        return enemigoMasCercano;
    }

    // Activar el cooldown entre ataques
    IEnumerator ActivarCooldown()
    {
        enCooldown = true;
        yield return new WaitForSeconds(tiempoCooldown);
        enCooldown = false;
    }

    // Método para recibir daño
    public void RecibirDanio(float cantidad)
    {
        if (estaMuerto) return;

        vida -= cantidad;

        if (vida <= 0)
        {
            Morir();
        }
    }

    // Manejar la muerte del jugador
    void Morir()
    {
        estaMuerto = true;
        Debug.Log("Jugador ha muerto");
        Destroy(gameObject); // Eliminar el jugador
        FindObjectOfType<GeneradorEnemigos>().DetenerGeneracion(); // Detener la generación de enemigos
    }

    // Visualizar el alcance de ataque en el Editor de Unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceDeAtaque);
    }
}