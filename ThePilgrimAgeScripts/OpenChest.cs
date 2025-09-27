using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] GameObject closedChest;
    [SerializeField] GameObject openChest;
    [SerializeField] GameObject key;
    bool isIn;
    public BasicMove move;

    private void Update()
    {
        if (isIn && move.isInteractPressed)
        {
            closedChest.SetActive(false);
            openChest.SetActive(true);
            key.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isIn = true;
        }
    }
}
