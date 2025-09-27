using System;
using TMPro;
using UnityEngine;

public class Temporizador : MonoBehaviour
{
    public float tiempo;
    private bool activo = false;

    public GameObject canvas;
    public TextMeshProUGUI texto;
    public GrabObjects gO;

    public event Action<GameObject> seAcaboTiempo;

    void Update()
    {
        if (activo)
        {
            canvas.SetActive(true);
            tiempo -= Time.deltaTime;
            texto.text = tiempo.ToString("F0");
            if (tiempo <= 0)
            {
                activo = false;
                seAcaboTiempo?.Invoke(gameObject);
                gO.ResetearBrazos();
            }
        }

        if (!activo)
        {
            canvas.SetActive(false);
        }
    }

    public void ActivarTemporizador(float tiempoInicial)
    {
        tiempo = tiempoInicial;
        activo = true;
    }

    public void ResetearTemporizador()
    {
        tiempo = 0;
        activo = false;
    }
}
