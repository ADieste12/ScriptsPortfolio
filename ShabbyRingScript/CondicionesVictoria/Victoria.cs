using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victoria : MonoBehaviour
{
    static int partidasParaGanar;
    static int partidasJugadas;
    static int partidasMaximas;
    static int j1Ganadas;
    static int j2Ganadas;

    int escenaActual;
    int numJugador;
    int jugadoresCaidos;
    bool resolviendoRonda;

    public GameObject puntuacion;
    public TextMeshProUGUI j1Text;
    public TextMeshProUGUI j2Text;

    public GameObject textovictoria;
    public GameObject textovictoria2;

    public GameObject[] jugadores;
    
    public Limites muerte;
    public List<Temporizador> temporizadores;
    Temporizador temp;
    public Escenas esc;
    public static Victoria instance;

    private void Awake()
    {
        //Le decimos que este es el unico game object con este script en escena y que no se destruya entre escenas, los hijos tampoco se destruyen
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        escenaActual = SceneManager.GetActiveScene().buildIndex;
        jugadoresCaidos = 0;
        resolviendoRonda = false;

        if (escenaActual >= 2 && !puntuacion.activeInHierarchy)
        {
            puntuacion.SetActive(true);
        }
        else
        {
            if (escenaActual <= 1 && puntuacion.activeInHierarchy)
            {
                puntuacion.SetActive(false);
            }
        }

        //Volvemos a referenciar los limites del escenario
        muerte = GameObject.FindGameObjectWithTag("ZonaMuerte")?.GetComponent<Limites>();
        if (muerte != null)
        {
            muerte.jugadorCaido += SumarPuntos;
        }

        if (escenaActual >= 2 && jugadores.Length == 0)
        {
            CargarJugadores();
        }
    }

    public void CargarJugadores()
    {
        jugadores = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < jugadores.Length; i++)
        {
            temp = jugadores[i].GetComponent<Temporizador>();
            temporizadores.Add(temp);
            temp.seAcaboTiempo += SumarPuntos;
        }
    }

    public void SumarPuntos(GameObject perdedor)
    {
        numJugador = perdedor.GetComponent<BasicMove>().numeroJugador;
        jugadoresCaidos++;

        if (!resolviendoRonda)
        {
            resolviendoRonda = true;
            StartCoroutine(EsperarGanador());
        }
    }

    public void CambiarEscena()
    {
        esc.EscenaAleatoria();
    }

    public void Inicio()
    {
        j1Ganadas = 0;
        j1Text.text = j1Ganadas.ToString();
        j2Ganadas = 0;
        j2Text.text = j2Ganadas.ToString();
        jugadores = new GameObject[0];
        puntuacion.SetActive(false);
        textovictoria.SetActive(false);
        textovictoria2.SetActive(false);
        Seleccionador.numJugador = 0;
        esc.CargarEscenaInicial();
    }

    IEnumerator EsperarGanador()
    {
        yield return new WaitForSeconds(2);
        if (jugadoresCaidos == 1)
        {
            if (escenaActual >= 2)
            {
                if (numJugador == 1)
                {
                    j2Ganadas++;
                    j2Text.text = j2Ganadas.ToString();
                }

                if (numJugador == 2)
                {
                    j1Ganadas++;
                    j1Text.text = j1Ganadas.ToString();
                }
            }
        }

        if (j1Ganadas >= 5 || j2Ganadas >= 5)
        {
            if (j1Ganadas >= 5)
                textovictoria.SetActive(true);
            if (j2Ganadas >= 5)
                textovictoria2.SetActive(true);
            Invoke("Inicio", 5f);
        }
        else
        {
            CambiarEscena();
        }
    }
}
