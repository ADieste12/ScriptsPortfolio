using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Farolillo : MonoBehaviour
{
    public DayNight dN;
    public BasicMove bM;
    public PlayerLife pL;
    void Update()
    {
       
        if (!dN.isDay && bM.isInteractPressed && dN.isInside)
        {
            dN.isDay = true;
        }
        else if (dN.isDay && bM.isInteractPressed && dN.isInside)
        {
            dN.isDay = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {   
        if (other.CompareTag("Player"))
        {
            dN.isInside = true;
            pL.life = 6;
        }            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dN.isInside = false;
        }
    }
}
