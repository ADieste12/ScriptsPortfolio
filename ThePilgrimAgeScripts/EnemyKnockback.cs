using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;

public class EnemyKnockback : MonoBehaviour
{
    Rigidbody2D rb;
    public float strength;
    [SerializeField] float delay = 0.15f;
    public int life = 4;
    public UnityEvent OnBegin, OnDone;
    bool canBeHitted = true;
    public GameObject door1;
    public GameObject door2;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
            if (door1 != null && door2 != null)
            {
                Destroy(door1);
                Destroy(door2);
            }
        }     
    }
    public void PlayFeedbackBunny(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce((direction) * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
        StartCoroutine(Invulnerability());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && canBeHitted)
        {
            life--;
            PlayFeedbackBunny(collision.gameObject);
        }
    }
    IEnumerator Invulnerability()
    {
        canBeHitted = false;
        yield return new WaitForSeconds(0.2f);
        canBeHitted = true;
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.linearVelocity = Vector3.zero;
        OnDone?.Invoke();
    }
}
