using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public int cantidadMaximaDeEnemigos = 10; // Límite máximo de enemigos
    private int cantidadActualDeEnemigos = 0; // Contador de enemigos actuales
    public float tiempoEntreGeneraciones = 2f; // Tiempo entre cada generación

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 0f, tiempoEntreGeneraciones);
    }

    void GenerarEnemigo()
    {
        if (cantidadActualDeEnemigos < cantidadMaximaDeEnemigos) // Verifica si hay espacio para más enemigos
        {
            // Instanciar un nuevo enemigo
            Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
            cantidadActualDeEnemigos++; // Aumentar el contador de enemigos
        }
        else
        {
            // Si se ha alcanzado el máximo de enemigos, no genera más
            CancelInvoke("GenerarEnemigo"); // Detener la generación de enemigos
        }
    }
}
