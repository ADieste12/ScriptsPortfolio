using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Limites : MonoBehaviour
{
    CinemachineTargetGroup target;
    ZonaItems items;
    Transform player;
    GameObject item;

    public event Action<GameObject> jugadorCaido;

    private void Start()
    {
        GameObject targetObj = GameObject.FindGameObjectWithTag("TargetGroup");
        if (targetObj != null)
        {
            target = targetObj.GetComponent<CinemachineTargetGroup>();
        }
        //items = GameObject.FindGameObjectWithTag("ZonaItems").GetComponent<ZonaItems>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            player = collision.transform;
            Invoke("QuitarPersonaje", 0);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Object"))
        {
            collision.GetComponent<Items>().destrozado = true;
            item = collision.gameObject;
            Invoke("DestruirItem", 1f);
        }
    }

    public void QuitarPersonaje()
    {
        //Si el jugador cae al vacio le desactivamos el movimento y lo quitamos del target group de la camara
        player.GetComponent<BasicMove>().enabled = false;
        if (target != null)
        {
            target.RemoveMember(player);
        }
        jugadorCaido?.Invoke(player.gameObject);
    }

    public void DestruirItem()
    {
        Destroy(item);
    }
}
