using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private Jugador jugador;
    private float vidaMaxima;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador").GetComponent<Jugador>();
        vidaMaxima = jugador.vida;
    }

    // Update is called once per frame
    void Update()
    {
        rellenoBarraVida.fillAmount = jugador.vida / vidaMaxima;
    }
}
