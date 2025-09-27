using Unity.VisualScripting;
using UnityEngine;

public class RightCollide : MonoBehaviour
{
    public bool isLeftHereR = false;
    public bool isRightHereR = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            isLeftHereR = true;
        }
        if (collision.CompareTag("Right"))
        {
            isRightHereR = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            isLeftHereR = false;
        }
        if (collision.CompareTag("Right"))
        {
            isRightHereR = false;
        }
    }
}