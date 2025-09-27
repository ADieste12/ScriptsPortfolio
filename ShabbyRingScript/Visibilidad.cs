using UnityEngine;
using UnityEngine.Tilemaps;
public class Visibilidad : MonoBehaviour
{
   // public GameObject ring;
   // public GameObject lona;
    public Tilemap ring;
    public Color color;
    Animator ringAnim;
    public int nplayers = 0;

    private void Start()
    {
        ringAnim = ring.GetComponent<Animator>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ringAnim != null) ringAnim.SetBool("change", true);
            nplayers++;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nplayers--;
            if (nplayers == 0)
            {
                if(ringAnim != null) ringAnim.SetBool("change", false);
            }
            
        }
    }

}
