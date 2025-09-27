using UnityEngine;

public class Casette : MonoBehaviour
{
    private Animator casette;
    public GameManager gameManager; 
    public int empezar;

    void Start()
    {
        casette = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            casette.SetTrigger("Click");
        }
    }
}
