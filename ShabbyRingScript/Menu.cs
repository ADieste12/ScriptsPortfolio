using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Escena()
    {
        SceneManager.LoadScene(1);
    }

    public void Cerrar()
    {
        Application.Quit();
    }
}
