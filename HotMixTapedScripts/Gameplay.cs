using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class Gameplay : MonoBehaviour
{
    public int click = 0;
    public GameObject pauseUI;
    private Button button;

    void Start()
    {
        StartCoroutine("Intermission");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click++;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
    }

    IEnumerator Intermission()
    {
        yield return new WaitForSeconds(3);
        if (click == 0)
        {
            SceneManager.LoadScene(4);
        }
        else if (click == 1)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(4);
        }
    }
    public void Continue(int cont)
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void MainMenu(int menu)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
