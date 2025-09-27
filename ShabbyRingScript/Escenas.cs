using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Escenas : MonoBehaviour
{
    public int escenaAleatoria;
    public int escenaActual;
    public int escenaAnterior;
    public int numeroEscenas = 5;
    ConstraintSource source;

    public bool jugadoresUnidos = false;
    public GameObject[] jugadores;

    public static Escenas instance;

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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EscenaAleatoria()
    {
        escenaAnterior = escenaActual;
        escenaActual = SceneManager.GetActiveScene().buildIndex;

        do
        {
            escenaAleatoria = Random.Range(2, numeroEscenas);
        }
        while (escenaAleatoria == escenaActual || escenaAleatoria == escenaAnterior);

        SceneManager.LoadScene(escenaAleatoria);
    }

    public void CargarEscenaInicial()
    {
        SceneManager.LoadScene(0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex >= 2)
        {
            PosicionarJugadores();
        }
        else
        {
            if (scene.buildIndex == 0 && jugadores.Length != 0)
            {
                for (int i = 0; i < jugadores.Length; i++)
                {
                    Destroy(jugadores[i]);
                }
                jugadores = new GameObject[0];
            }
        }
    }

    public void PosicionarJugadores()
    {
        if (jugadores.Length == 0)
        {
            jugadores = GameObject.FindGameObjectsWithTag("Player");
        }

        GameObject[] posicionInicial = GameObject.FindGameObjectsWithTag("SpawnPlayer");
        GameObject[] focos = GameObject.FindGameObjectsWithTag("Foco");
        CinemachineTargetGroup target = GameObject.FindGameObjectWithTag("TargetGroup").GetComponent<CinemachineTargetGroup>();

        for (int i = 0; i < jugadores.Length; i++)
        {
            jugadores[i].GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            jugadores[i].transform.position = posicionInicial[i].transform.position;

            //Asignarle un foco del array
            source.sourceTransform = jugadores[i].transform;
            source.weight = 1.0f;
            focos[i].GetComponent<AimConstraint>().AddSource(source);

            //Añadir el target a la cinemachine group
            target.AddMember(jugadores[i].transform, 1, 1);

            //Activar el moviento del jugador
            jugadores[i].GetComponent<BasicMove>().enabled = true;

            //Si tiene un objeto agarrado que lo suelte
            GameObject item = jugadores[i].GetComponent<GrabObjects>().itemAgarrado;
            GrabObjects gb = jugadores[i].GetComponent<GrabObjects>();
            if (item != null)
            {
                item.GetComponent<Items>().DestruirItem();
                item = null;
                gb.ResetearBrazos();
            }
        }
    }
}
