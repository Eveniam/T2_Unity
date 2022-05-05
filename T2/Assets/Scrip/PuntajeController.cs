using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuntajeController : MonoBehaviour
{
    
    public Text PuntajeTexto;
    public Text Vida;
    private int puntaje, vida = 3;

    public void IncrementarPuntaje(int puntos)
    {
        puntaje += puntos;
    }
    public void DisminuirVida(int vid)
    {
        vida -= vid;
    }
    private void Update()
    {
        PuntajeTexto.text = "POINTS: " + puntaje;
        Vida.text = "VIDA -- " + vida;
    }
}
