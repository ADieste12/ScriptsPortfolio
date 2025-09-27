using UnityEngine;

public class BossMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public LayerMask wall;
    public float sideLength;
    public Transform sideController;
    public bool sideInfo;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    //ENEMIGO QUE SALTA CONSTANTEMENTE, SI TOCA EL SUELO VUELVE A SALTAR
    void Update()
    {
        if (rb.linearVelocityY == 0f)
            rb.linearVelocity = new Vector2(speed, jumpForce);
        else 
            rb.linearVelocity.Normalize();
        sideInfo = Physics2D.Raycast(sideController.position, transform.right, sideLength, wall);

        if (sideInfo)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(sideController.transform.position, sideController.transform.position + transform.right * sideLength);
    }
}