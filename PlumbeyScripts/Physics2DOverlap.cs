using UnityEngine;

public class Physics2DOverlap : MonoBehaviour
{
    [SerializeField] Transform objetoDerecho;
    [SerializeField] float radioDerecho;
    [SerializeField] float angleDerecho;
    [SerializeField] float longLineDerecho;   
    [SerializeField] Transform objetoIzquierdo;
    [SerializeField] float radioIzquierdo;
    [SerializeField] float angleIzquierdo;
    [SerializeField] float longLineIzquierdo;
    [SerializeField] LayerMask mascaraLayer;
    [SerializeField] LayerMask mascaraLayerWall;
    [SerializeField] LayerMask mascaraLayerWallLeft;
    [SerializeField] LayerMask mascaraLayerCeiling;
    public bool circleDerecho;
    public bool circleIzquierdo;
    [SerializeField] GameObject rotIzquierda;
    [SerializeField] GameObject rotDerecha;
    public bool leftLegContact;
    public bool rightLegContact;
    public bool rightLegContactWall;
    public bool leftLegContactWall;
    public bool rightLegContactWallLeft;
    public bool leftLegContactWallLeft;
    public bool rightLegContactCeiling;
    public bool leftLegContactCeiling;
    int cambioRot = 0;
    public int gravityChange;

    void Update()
    {
        Vector2 gravedad = Physics2D.gravity;
        Debug.Log(gravedad);
        leftLegContact = Physics2D.OverlapCircle(objetoIzquierdo.position, radioIzquierdo, mascaraLayer);
        rightLegContact = Physics2D.OverlapCircle(objetoDerecho.position, radioDerecho, mascaraLayer);
        rightLegContactWall = Physics2D.OverlapCircle(objetoDerecho.position, radioDerecho, mascaraLayerWall);
        leftLegContactWall = Physics2D.OverlapCircle(objetoIzquierdo.position, radioIzquierdo, mascaraLayerWall);
        rightLegContactWallLeft = Physics2D.OverlapCircle(objetoDerecho.position, radioDerecho, mascaraLayerWallLeft);
        leftLegContactWallLeft = Physics2D.OverlapCircle(objetoIzquierdo.position, radioIzquierdo, mascaraLayerWallLeft);
        rightLegContactCeiling = Physics2D.OverlapCircle(objetoDerecho.position, radioDerecho, mascaraLayerCeiling);
        leftLegContactCeiling = Physics2D.OverlapCircle(objetoIzquierdo.position, radioIzquierdo, mascaraLayerCeiling);
        if (Input.GetMouseButtonDown(1) && gravityChange != 0)
        {
            gravityChange = 0;
        }
        if (gravityChange == 0)
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
        if (leftLegContact && rightLegContact && cambioRot <= 0 && gravityChange == 0)
        {
            cambioRot++;
            rotIzquierda.transform.position = new Vector3(objetoDerecho.position.x, objetoDerecho.position.y, transform.position.z);
            rotDerecha.transform.position = new Vector3(objetoIzquierdo.position.x, objetoIzquierdo.position.y, transform.position.z);
        }
        if ((leftLegContactWall || rightLegContactWall) || (leftLegContactWall && rightLegContactWall) && cambioRot <= 0)
        {
            if (Input.GetMouseButton(0) && gravityChange != 1)
            {
                gravityChange = 1;
            }
            if (gravityChange == 1)
            {
                cambioRot++;
                rotIzquierda.transform.position = new Vector3(objetoDerecho.position.x, objetoDerecho.position.y, transform.position.z);
                rotDerecha.transform.position = new Vector3(objetoIzquierdo.position.x, objetoIzquierdo.position.y, transform.position.z);
                Physics2D.gravity = new Vector2(9.81f, 0);
            }
        }
        if ((leftLegContactWallLeft || rightLegContactWallLeft) || (leftLegContactWallLeft && rightLegContactWallLeft) && cambioRot <= 0)
        {
            if (Input.GetMouseButton(0) && gravityChange != 3)
            {
                gravityChange = 3;
            }
            if (gravityChange == 3)
            {
                cambioRot++;
                rotIzquierda.transform.position = new Vector3(objetoDerecho.position.x, objetoDerecho.position.y, transform.position.z);
                rotDerecha.transform.position = new Vector3(objetoIzquierdo.position.x, objetoIzquierdo.position.y, transform.position.z);
                Physics2D.gravity = new Vector2(-9.81f, 0);
            }
        }
        if ((leftLegContactCeiling || rightLegContactWall) || (leftLegContactWall && rightLegContactWall) && cambioRot <= 0)
        {
            if (Input.GetMouseButton(0) && gravityChange != 2)
            {
                Debug.Log("Pulsado");
                gravityChange = 2;
            }
            if (gravityChange == 2)
            {
                cambioRot++;
                rotIzquierda.transform.position = new Vector3(objetoDerecho.position.x, objetoDerecho.position.y, transform.position.z);
                rotDerecha.transform.position = new Vector3(objetoIzquierdo.position.x, objetoIzquierdo.position.y, transform.position.z);
                Physics2D.gravity = new Vector2(0, 9.81f);
            }
        }
        if (!leftLegContact || !rightLegContact)
        {
            cambioRot = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (circleDerecho)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(objetoDerecho.position, radioDerecho);
        }
        if (circleIzquierdo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(objetoIzquierdo.position, radioIzquierdo);
        }
    }
}
