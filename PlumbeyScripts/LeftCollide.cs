using Unity.VisualScripting;
using UnityEngine;

public class LeftCollide : MonoBehaviour
{
    public bool isLeftHereL = false;
    public bool isRightHereL = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            isLeftHereL = true;
        }
        if (collision.CompareTag("Right"))
        {
            isRightHereL = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            isLeftHereL = false;
        }
        if (collision.CompareTag("Right"))
        {
            isRightHereL = false;
        }
    }
}