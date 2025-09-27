using UnityEngine;

public class BossLance : MonoBehaviour
{
    public int speed;
    void Update()
    {
        //VELOCIDAD DE LA LANZA Y DIRECCION
        Vector2 direction = (-Vector2.right * speed * Time.deltaTime);
        transform.Translate(direction);
    }
}
