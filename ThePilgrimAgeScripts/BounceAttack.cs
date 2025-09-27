using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BounceAttack : MonoBehaviour
{
    public Rigidbody2D rb;
    public BasicMove bM;
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // SI HACES EL ATAQUE PARA ABAJO Y LE DAS A UN ENEMIGO TIENES UN REBOTE
        if (other.CompareTag("Enemy"))
        {
            bM.isBouncing = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 25f);
            StartCoroutine(DeactivateCollider());
        }
        if (other.CompareTag("Boss"))
        {
            bM.isBouncing = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 25f);
            StartCoroutine(DeactivateCollider());
        }
    }

    IEnumerator DeactivateCollider()
    {
        yield return new WaitForSeconds(0.2f);
        col.enabled = false;
    }
}
