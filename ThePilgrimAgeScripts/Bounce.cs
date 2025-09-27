using System.Collections;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounce;
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //DEPENDIENDO DE LA VELOCIDAD A LA QUE VAYA EL PERSONAJE EN Y LA FUERZA DE REBOTE ES MAYOR O MENOR
            if (other.attachedRigidbody.linearVelocity.y > 20f)
            {
                bounce = 5f;
            }
            if (other.attachedRigidbody.linearVelocity.y >= 15f && other.attachedRigidbody.linearVelocity.y <= 20f)
            {
                bounce = 15f;
            }
            else if (other.attachedRigidbody.linearVelocity.y < 15f && other.attachedRigidbody.linearVelocity.y >= 0f)
            {
                bounce = 25f;
            }
            else if (other.attachedRigidbody.linearVelocity.y < 0f && other.attachedRigidbody.linearVelocity.y > -10.5f)
            {
                bounce = 35f;
            }
            else if (other.attachedRigidbody.linearVelocity.y <= -10.5f && other.attachedRigidbody.linearVelocity.y > -17f)
            {
                bounce = 45f;
            }
            else if (other.attachedRigidbody.linearVelocity.y <= -17f)
            {
                bounce = 55f;
            }  
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up *  bounce, ForceMode2D.Impulse);
            StartCoroutine(DestroyCloud());
        }
    }
    public void Awake()
    {
        StartCoroutine(CloudTime());
    }

    IEnumerator DestroyCloud()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }

    IEnumerator CloudTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
