using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject optionsUI;
    public GameObject levelsUI;
    public Animator casette;
    int click = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            click++;
            if (click == 1)
            {
                StartCoroutine("Click");
            }
        }  
    }
    IEnumerator Click()
    {
        yield return new WaitForSeconds(4);
        menuUI.SetActive(true);
    }

    public void StartGame(int empezar)
    {
        StartCoroutine("Level");
        menuUI.SetActive(false);
        casette.SetTrigger("Jugar");
    }

    public void Options(int config)
    {
        optionsUI.SetActive(true);
    }

    public void CloseOptions(int configClose)
    {
        optionsUI.SetActive(false);
    }

    public void Select(int levels)
    {
        SceneManager.LoadScene(1);
    }
    IEnumerator Level()
    {
        yield return new WaitForSeconds(2);
        levelsUI.SetActive(true);
    }

    public void ExitGame(int exit)
    {
        Application.Quit();
    }
}
