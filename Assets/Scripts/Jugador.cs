using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float danio = 1f; // Daño del jugador al atacar
    public float alcanceDeAtaque = 2f; // Distancia desde el jugador para el ataque
    public float anguloDeAtaque = 60f; // Ángulo del triángulo de ataque
    public LayerMask capaEnemigos; // Capa de los enemigos
    public float tiempoCooldown = 1f; // Cooldown entre ataques
    private bool enCooldown = false; // Indica si el jugador está en cooldown
    public GameObject efectoTrianguloPrefab; // Prefab para el diseño visual del ataque

    void Update()
    {
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
        Collider2D[] enemigosEnRango = Physics2D.OverlapCircleAll(transform.position, alcanceDeAtaque, capaEnemigos);
        return enemigosEnRango;
    }

    // Realizar el ataque sobre los enemigos detectados
    void Atacar(Collider2D[] enemigos)
    {
        foreach (var enemigo in enemigos)
        {
            enemigo.GetComponent<Enemigo>().RecibirDanio(danio);
        }
    }

    // Mostrar efectos visuales del ataque en forma de triángulo
    void MostrarEfectosVisuales(Collider2D[] enemigos)
    {
        if (efectoTrianguloPrefab != null)
        {
            // Encontrar al enemigo más cercano
            Transform enemigoMasCercano = EncontrarEnemigoMasCercano(enemigos);

            if (enemigoMasCercano != null)
            {
                // Calcular la dirección hacia el enemigo más cercano
                Vector3 direccionAtaque = (enemigoMasCercano.position - transform.position).normalized;

                // Calcular la rotación para que el ataque apunte al enemigo
                float anguloRotacion = Mathf.Atan2(direccionAtaque.y, direccionAtaque.x) * Mathf.Rad2Deg;

                // Calcular la posición de aparición del efecto en el borde del triángulo
                Vector3 posicionEfecto = transform.position + direccionAtaque * alcanceDeAtaque;

                // Crear el efecto visual del triángulo en la posición calculada
                GameObject efecto = Instantiate(efectoTrianguloPrefab, posicionEfecto, Quaternion.Euler(0, 0, anguloRotacion));
                Destroy(efecto, 0.5f); // Destruir el efecto después de 0.5 segundos
            }
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
        enCooldown = true; // Activa el cooldown
        yield return new WaitForSeconds(tiempoCooldown); // Espera el tiempo de cooldown
        enCooldown = false; // Desactiva el cooldown
    }

    // Visualizar el alcance de ataque en el Editor de Unity (para ver el rango del triángulo)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alcanceDeAtaque); // Mostrar el radio del ataque
    }
}