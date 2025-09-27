using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    Rigidbody2D rb;
    public float strength;
    public int life = 6;
    [SerializeField] float delay = 0.15f;
    bool canBeHitted = true;
    public UnityEvent OnBegin, OnDone;
    public GameObject sixLifes;
    public GameObject fiveLifes;
    public GameObject fourLifes;
    public GameObject threeLifes;
    public GameObject twoLifes;
    public GameObject oneLife;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (life <= 0)
            life = 6;
        //INTERCAMBIO DE IMAGENES EN EL HUD DEPENDIENDO DE LA VIDA
        if (life == 6)
        {
            sixLifes.SetActive(true);
            fiveLifes.SetActive(false);
            fourLifes.SetActive(false);
            threeLifes.SetActive(false);
            twoLifes.SetActive(false);
            oneLife.SetActive(false);
        }
       else if (life == 5)
        {
            sixLifes.SetActive(false);
            fiveLifes.SetActive(true);
            fourLifes.SetActive(false);
            threeLifes.SetActive(false);
            twoLifes.SetActive(false);
            oneLife.SetActive(false);
        }
        else if (life == 4)
        {
            sixLifes.SetActive(false);
            fiveLifes.SetActive(false);
            fourLifes.SetActive(true);
            threeLifes.SetActive(false);
            twoLifes.SetActive(false);
            oneLife.SetActive(false);
        }
        else if (life == 3)
        {
            sixLifes.SetActive(false);
            fiveLifes.SetActive(false);
            fourLifes.SetActive(false);
            threeLifes.SetActive(true);
            twoLifes.SetActive(false);
            oneLife.SetActive(false);
        }
        else if (life == 2)
        {
            sixLifes.SetActive(false);
            fiveLifes.SetActive(false);
            fourLifes.SetActive(false);
            threeLifes.SetActive(false);
            twoLifes.SetActive(true);
            oneLife.SetActive(false);
        }
        else if (life == 1)
        {
            sixLifes.SetActive(false);
            fiveLifes.SetActive(false);
            fourLifes.SetActive(false);
            threeLifes.SetActive(false);
            twoLifes.SetActive(false);
            oneLife.SetActive(true);
        }
    }

    //DEPENDIENDO DE LA TAG QUE COLISIONE CONTRA EL JUGADOR EL DAÑO Y LA FUERZA DE EMPUJE VARÍA
    public void PlayFeedbackBunny(GameObject sender)
    {
        strength = 20;
        if (life > 0)
        {
            life--;
        }
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce((direction) * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }     
    public void PlayFeedbackLance(GameObject sender)
    {
        strength = 20;
        if (life > 0)
        {
            life--;
        }
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce((direction) * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }    
    public void PlayFeedbackBunnyBoss(GameObject sender)
    {
        life -= 2;
        strength = 30;
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce((direction) * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    public void PlayFeedbackFinalBoss(GameObject sender)
    {
        life -= 2;
        strength = 35;
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce((direction) * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BasicBunny") && canBeHitted)
        {
            PlayFeedbackBunny(collision.gameObject);
            StartCoroutine(InvulnerabilityFrames());
        }
        else if (collision.CompareTag("BunnyBoss") && canBeHitted)
        {
            PlayFeedbackBunnyBoss(collision.gameObject);
            StartCoroutine(InvulnerabilityFrames());
        }
        else if (collision.CompareTag("Boss") && canBeHitted)
        {
            PlayFeedbackFinalBoss(collision.gameObject);
            StartCoroutine(InvulnerabilityFrames());
        }
        else if (collision.CompareTag("Lance") && canBeHitted)
        {
            PlayFeedbackLance(collision.gameObject);
            StartCoroutine(InvulnerabilityFrames());
        }
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }

    //FRAMES DE INVULNEARABILIDAD
    IEnumerator InvulnerabilityFrames()
    {
        canBeHitted = false;
        yield return new WaitForSeconds(1f);
        canBeHitted = true;
    }
}