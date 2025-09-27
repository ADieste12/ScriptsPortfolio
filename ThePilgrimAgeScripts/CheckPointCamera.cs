using UnityEngine;

public class CheckPointCamera : MonoBehaviour
{
    public GameObject[] deactivateCamera;
    public GameObject activateCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //DESACTIVA TODAS LAS CAMARAS EXCEPTO LA DE LA ZONA DONDE ESTAS
            deactivateCamera[0].SetActive(false);
            deactivateCamera[1].SetActive(false);
            deactivateCamera[2].SetActive(false);
            deactivateCamera[3].SetActive(false);
            deactivateCamera[4].SetActive(false);
            deactivateCamera[5].SetActive(false);
            deactivateCamera[6].SetActive(false);
            deactivateCamera[7].SetActive(false);
            deactivateCamera[8].SetActive(false);
            deactivateCamera[9].SetActive(false);
            deactivateCamera[10].SetActive(false);
            deactivateCamera[11].SetActive(false);
            activateCamera.SetActive(true);
        }
    }
}
