using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private Jugador jugador;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
        jugador.MuerteJugador += ActivarMenu;
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        Debug.Log("El menú de Game Over se activó.");
        menuGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }

    public void Salir()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
