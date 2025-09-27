using UnityEngine;

public class Items : MonoBehaviour
{
    public bool destrozado = false;
    public int durabilidad;

    public void RestarDuravilidad()
    {
        durabilidad --;
        
        if (durabilidad <= 0)
        {
            destrozado = true;
            Invoke("DestruirItem", 0.1f);
        }
    }

    public void DestruirItem()
    {
        Destroy(gameObject);
    }
}
