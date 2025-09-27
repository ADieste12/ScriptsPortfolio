using UnityEngine;

public class DeactivateTutoCamera : MonoBehaviour
{
    public GameObject deactivateCamera;
    public GameObject activateCamera;

    //DESACTIVAR CAMAREA ANTERIOR Y ACTIVAR CORRESPONDIENTE
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            deactivateCamera.SetActive(false);
            activateCamera.SetActive(true);
        }
    }
}
