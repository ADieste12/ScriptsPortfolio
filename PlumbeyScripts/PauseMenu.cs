using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause;
    [SerializeField] GameObject levelSelector;
    [SerializeField] GameObject main;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
        audioManager.PlaySFX(audioManager.botonClick);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        Physics2D.gravity = new Vector2(0, -9.81f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    
    public void Play()
    {
        main.SetActive(false);
        levelSelector.SetActive(true);
    }
    public void Back()
    {
        main.SetActive(true);
        levelSelector.SetActive(false);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    
    public void Level1()
    {
        SceneManager.LoadScene(1);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level2()
    {
        SceneManager.LoadScene(2);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level3()
    {
        SceneManager.LoadScene(3);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level4()
    {
        SceneManager.LoadScene(4);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level5()
    {
        SceneManager.LoadScene(5);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level6()
    {
        SceneManager.LoadScene(6);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level7()
    {
        SceneManager.LoadScene(7);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level8()
    {
        SceneManager.LoadScene(8);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level9()
    {
        SceneManager.LoadScene(9);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }
    public void Level10()
    {
        SceneManager.LoadScene(10);
        Physics2D.gravity = new Vector2(0, -9.81f);
        audioManager.PlaySFX(audioManager.botonClick);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
