using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityChange : MonoBehaviour
{
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject reesplandor;
    [SerializeField] Collider2D retrete;
    GameObject candado;
    [SerializeField] Animator anim;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("Move", true);
            StartCoroutine(Animation());

            audioManager.PlaySFX(audioManager.cambioGravedad4);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Physics2D.gravity = new Vector2(0, 9.81f);
            transform.eulerAngles = new Vector3(0f, 0f, 180f);
            anim.SetBool("Move", true);
            StartCoroutine(Animation());

            audioManager.PlaySFX(audioManager.cambioGravedad4);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Physics2D.gravity = new Vector2(-9.81f, 0);
            transform.eulerAngles = new Vector3(0f, 0f, 270f);
            anim.SetBool("Move", true);
            StartCoroutine(Animation());

            audioManager.PlaySFX(audioManager.cambioGravedad5);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Physics2D.gravity = new Vector2(9.81f, 0);
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
            anim.SetBool("Move", true);
            StartCoroutine(Animation());

            audioManager.PlaySFX(audioManager.cambioGravedad5);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pause.SetActive(true);
            audioManager.PlaySFX(audioManager.ventosa3);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("Move", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Retrete"))
        {
            win.SetActive(true); 
            Time.timeScale = 0f;
            if (SceneManager.GetActiveScene().buildIndex == 10)
                StartCoroutine(ResetGame());

            audioManager.PlaySFX(audioManager.ganar);
        }
        if (collision.CompareTag("Obstaculo"))
        {
            lose.SetActive(true);
            Time.timeScale = 0f;
            StartCoroutine(ResetScene());

            audioManager.PlaySFX(audioManager.perder);
        }
        if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            reesplandor.SetActive(true);
            retrete.enabled = true;
            candado = GameObject.FindGameObjectWithTag("Candado");
            if (candado != null)
            {
                Destroy(candado);
            }
            audioManager.PlaySFX(audioManager.ventosa);
        }
    }

    public void ChangeScene()
    {
        Time.timeScale = 1f;
        Physics2D.gravity = new Vector2(0, -9.81f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator ResetScene()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        Physics2D.gravity = new Vector2(0, -9.81f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ResetGame()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        Physics2D.gravity = new Vector2(0, -9.81f);
        SceneManager.LoadScene(0);
    }
}
