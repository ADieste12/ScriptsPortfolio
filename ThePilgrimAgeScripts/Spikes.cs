using UnityEngine;
using UnityEngine.UIElements;

public class TutorialSpikes : MonoBehaviour
{
    Vector3 translate;
    public Transform teleport;
    public PlayerLife life;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CUANDO CAES A LOS PINCHOS TE DEVUELVE A UNA ZONA SEGURA ANTERIOR A LA CAIDA Y PIERDES 1 PUNTO DE VIDA
        if (collision.CompareTag("Player"))
        {
            translate = teleport.position;
            life.life--;
            collision.transform.position = translate;
        }
    }
}
