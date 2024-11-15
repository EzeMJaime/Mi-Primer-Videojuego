using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public int cantidadMaximaDeEnemigos = 10;
    private int cantidadActualDeEnemigos = 0;
    public float tiempoEntreGeneraciones = 2f;
    private bool activo = true;

    void Start()
    {
        InvokeRepeating("GenerarEnemigo", 0f, tiempoEntreGeneraciones);
    }

    void GenerarEnemigo()
    {
        if (!activo || cantidadActualDeEnemigos >= cantidadMaximaDeEnemigos) return;

        Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
        cantidadActualDeEnemigos++;
    }

    public void DetenerGeneracion()
    {
        activo = false;
        CancelInvoke("GenerarEnemigo");
        Debug.Log("Generación de enemigos detenida");
    }
}