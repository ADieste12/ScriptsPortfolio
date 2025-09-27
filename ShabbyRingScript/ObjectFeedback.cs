using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectFeedback : MonoBehaviour
{
    GrabObjects gO;
    BasicMove bM;
    public int stun;
    public bool isStuned = false;
    public bool isHitted = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Start()
    {
        gO = GetComponent<GrabObjects>();
        bM = GetComponent<BasicMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Codo") && !isHitted)
        {
            RestarStun(1);
            audioManager.PlaySFX(audioManager.codazo);
        }
    }

    public void RestarStun(int daño)
    {
        stun -= daño;
        if (stun <= 0 && !isStuned)
        {
            this.gameObject.tag = "pickPlayer";
            isStuned = true;
            StartCoroutine(Stuned());
        }
        else 
        { 
            isHitted = true;
            Invoke("Hitted", 0.4f);
        }
    }

    public void Hitted()
    {
        isHitted = false;
    }

    IEnumerator Stuned()
    {
        yield return new WaitForSeconds(2.5f);
        stun = 4;
        isStuned = false;
        this.gameObject.tag = "Player";
        if (bM.enabled == true)
        {
            gO.enabled = true;
        }
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        stun = 4;
    }
}