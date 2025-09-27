using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class Seleccionador : MonoBehaviour
{
    public GameObject[] jugadores;
    public static int numJugador;
    GameObject[] posicionInicial;
    public PlayerInput input;

    /*public GameObject[] focos;
    ConstraintSource source;
    CinemachineTargetGroup target;*/
    
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        posicionInicial = GameObject.FindGameObjectsWithTag("SpawnPlayer");

        //Instanciar jugador con el player input, y posicionarlo en su zona. Le ponemos que no se destruya al cambiar de escena
        PlayerInput jugador = PlayerInput.Instantiate(jugadores[numJugador], controlScheme: input.currentControlScheme, pairWithDevice: input.devices[0]);
        DontDestroyOnLoad(jugador.gameObject);
        jugador.transform.position = posicionInicial[numJugador].transform.position;

        numJugador++;
        jugador.GetComponent<BasicMove>().numeroJugador = numJugador;
        Destroy(gameObject);

        /*focos = GameObject.FindGameObjectsWithTag("Foco");
        target = GameObject.FindGameObjectWithTag("TargetGroup").GetComponent<CinemachineTargetGroup>();

        //Asignarle un foco del array
        source.sourceTransform = jugador.transform;
        source.weight = 1.0f;
        focos[numJugador].GetComponent<AimConstraint>().AddSource(source);

        //Añadir el target a la cinemachine group
        target.AddMember(jugador.transform, 1, 1);*/
    }
}
