using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public LayerMask ground;
    public LayerMask wall;
    public float downLength;
    public float sideLength;
    public Transform downController;
    public Transform sideController;
    public bool downInfo;
    public bool sideInfo;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2 (speed, rb.linearVelocity.y);
        //DETECCION DE DISTINTAS LAYERS PARA CAMBIAR DE UN LADO A OTRO ANDANDO Y NO CAERSE O QUEDARSE ANDANDO CONTRA UNA PARED
        sideInfo = Physics2D.Raycast(sideController.position, transform.right, sideLength, wall);
        downInfo = Physics2D.Raycast(downController.position, transform.up * -1, downLength, ground);

        if (!downInfo || sideInfo)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(downController.transform.position, downController.transform.position + transform.up * -1 * downLength);
        Gizmos.DrawLine(sideController.transform.position, sideController.transform.position + transform.right * sideLength);
    }
}