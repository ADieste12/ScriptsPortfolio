using UnityEngine;

public class Hill : MonoBehaviour
{
    public BasicMove bM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bM.enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.4f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bM.enabled = true;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 7f;
        }
    }
}
