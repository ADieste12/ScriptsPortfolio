using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public GameObject[] zonaSpawn;
    public GameObject[] itemsPrefab;
    int itemAleatorio;

    private void Start()
    {
        zonaSpawn = GameObject.FindGameObjectsWithTag("ZonaItems");
        for (int i = 0; i < zonaSpawn.Length; i++)
        {
            //Recorro todas las zonas de spawn y le pongo un item aleatorio si no tiene ninguno asignado
            ZonaItems zi = zonaSpawn[i].GetComponent<ZonaItems>();

            if (zi.item == null)
            {        
                do
                {
                    itemAleatorio = Random.Range(0, itemsPrefab.Length);
                }
                while ((zi.itemExcluido != null) && (zi.itemExcluido == itemsPrefab[itemAleatorio]));
                zi.item = itemsPrefab[itemAleatorio];
            }
        }
    }
}
