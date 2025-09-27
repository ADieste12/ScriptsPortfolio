using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    public float strength;
    [SerializeField] float delay = 0.15f;
    public ObjectFeedback stuned;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender, float fuerza)
    {
        if (stuned.stun <= 0)
        {
            fuerza *= 2;
        }
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * fuerza, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    /*
    public void PlayFeedbackMaletin (GameObject sender)
    {
        if (stuned.stun > 0)
            strength = 4;
        else if (stuned.stun <= 0)
            strength = 8;
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    } 
    public void PlayFeedbackSilla (GameObject sender)
    {
        if (stuned.stun > 0)
            strength = 4;
        else if (stuned.stun <= 0)
            strength = 8;
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    } 
    public void PlayFeedbackGlove (GameObject sender)
    {
        if (stuned.stun > 0)
            strength = 16;
        else if (stuned.stun <= 0)
            strength = 26;
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }*/
    public void PlayFeedbackThrow(GameObject sender)
    {
        strength = 40;
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce((-direction) * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Throw"))
        {
            PlayFeedbackThrow(collision.gameObject);
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
