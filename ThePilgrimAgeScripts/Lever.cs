using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Lever : MonoBehaviour
{
    public BasicMove bM;
    bool isInside;
    public GameObject door;
    SpriteRenderer sP;
    private void Awake()
    {
        sP = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (bM.isInteractPressed && isInside)
        {
            sP.flipX = true;
            Destroy(door);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {
            isInside = true;
        }            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
        }
    }
}
