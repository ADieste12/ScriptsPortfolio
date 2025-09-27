using System.Collections;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Transform posicionFinal;
    float posY;
    Vector2 pos;
   
    void Start()
    {
        posY = transform.position.y;
        StartCoroutine(Subir());
    }

    IEnumerator Subir()
    {
        yield return new WaitForSeconds(2);

        while (Vector2.Distance(transform.position, posicionFinal.position) > 0.01f)
        {
            posY += 0.005f;
            pos = new Vector2(0, posY);
            transform.position = pos;

            yield return null;
        }

        transform.position = posicionFinal.position;
    }
}
