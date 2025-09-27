using Unity.VisualScripting;
using UnityEngine;

public class LeftCollideWall : MonoBehaviour
{
    public bool isLeftHere = false;
    public bool isRightHere = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            isLeftHere = true;
        }
        if (collision.CompareTag("Right"))
        {
            isRightHere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            isLeftHere = false;
        }
        if (collision.CompareTag("Right"))
        {
            isRightHere = false;
        }
    }
}