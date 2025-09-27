using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] BasicMove bM;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        bM.isPaused = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
