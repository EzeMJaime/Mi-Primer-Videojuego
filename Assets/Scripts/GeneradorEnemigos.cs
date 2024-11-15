using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab; // Prefab del enemigo
    public int cantidadMaximaDeEnemigos = 10; // L�mite m�ximo de enemigos
    private int cantidadActualDeEnemigos = 0; // Contador de enemigos actuales
    public float tiempoEntreGeneraciones = 2f; // Tiempo entre cada generaci�n

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 0f, tiempoEntreGeneraciones);
    }

    void GenerarEnemigo()
    {
        if (cantidadActualDeEnemigos < cantidadMaximaDeEnemigos) // Verifica si hay espacio para m�s enemigos
        {
            // Instanciar un nuevo enemigo
            Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
            cantidadActualDeEnemigos++; // Aumentar el contador de enemigos
        }
        else
        {
            // Si se ha alcanzado el m�ximo de enemigos, no genera m�s
            CancelInvoke("GenerarEnemigo"); // Detener la generaci�n de enemigos
        }
    }
}
