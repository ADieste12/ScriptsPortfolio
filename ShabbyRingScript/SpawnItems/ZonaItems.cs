using Unity.VisualScripting;
using UnityEngine;

public class ZonaItems : MonoBehaviour
{
    public int tiempRespawn;
    public GameObject item; //Si el item es aleatorio dejarlo vacio
    public GameObject itemExcluido; //item que no puede salir en este lugar
    GameObject itemInstanciado;
    Items scrip;
    bool objetoPerdido;

    private void Start()
    {
        Invoke("Spawnear", 1f);
    }

    private void Update()
    {
        if (itemInstanciado != null)
        {
            objetoPerdido = scrip.destrozado;
        }

        if (objetoPerdido && !IsInvoking("Spawnear"))
        {
            objetoPerdido = false;
            Invoke("Spawnear", tiempRespawn);
        }
    }

    public void Spawnear()
    {
        itemInstanciado = Instantiate(item, transform);
        scrip = itemInstanciado.GetComponent<Items>();
    }
}
