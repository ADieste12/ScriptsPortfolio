using System.Collections.Generic;
using UnityEngine;

public class MantenerZona : MonoBehaviour
{
    public float tiempoMaximo = 20;
    public List<GameObject> players;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            print("entrar");
            players.Add(collision.gameObject);
            collision.GetComponent<Temporizador>().ActivarTemporizador(tiempoMaximo);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            players.Remove(collision.gameObject);
            collision.GetComponent<Temporizador>().ResetearTemporizador();
        }
    }
}
