using System.Collections;
using UnityEngine;

public class FinalBossMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public LayerMask wall;
    public float sideLength;
    public Transform sideController;
    public bool sideInfo;
    public bool isAttacking;
    public bool canRun;
    public bool canAttack = true;
    public bool canThrow;
    public GameObject lance;
    GameObject[] lances;
    public GameObject lancePosition;
    int lanceAmmount;
    Animator anim;
    public EnemyKnockback eK;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Attack", isAttacking);
        lances = GameObject.FindGameObjectsWithTag("Lance");
        lanceAmmount = GameObject.FindGameObjectsWithTag("Lance").Length;
        if (isAttacking)
        {
            StartCoroutine(PrepareForAttack());
            if (canRun)
                rb.linearVelocity = new Vector2(-speed, rb.linearVelocityY);
        }

        if (canThrow)
        {
            if (lanceAmmount < 3)
            {
                canAttack = false;
                StartCoroutine(Throwing());
            }
            if (lanceAmmount >= 3)
            {
                StartCoroutine(DestroyLances());
                canThrow = false;
            }
        }

        sideInfo = Physics2D.Raycast(sideController.position, transform.right, sideLength, wall);

        if (sideInfo)
        {
            rb.linearVelocity = Vector2.zero;
            ChangeDirection();
            isAttacking = false;
            canAttack = true;
            canRun = false;
        }
        if (canAttack)
            StartCoroutine(Attacking());
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

    IEnumerator Attacking()
    {
        canAttack = false;
        //CUANDO TIENE MENOS VIDA HACE EL PROCESO MAS RAPIDO MARCANDO ASI UNA SEGUNDA FASE
        if (eK.life > 10)
        {
            yield return new WaitForSeconds(1f);
        }
        else if (eK.life <= 10)
        {
            yield return new WaitForSeconds(0.4f);
        }
        //CREA UN NUMERO ALEATORIO ENTRE 0 Y 2 QUE DETERMINA SU SIGUIENTE PATRON DE ATAQUE
        int initiateAttack = Random.Range(0, 3);
        Debug.Log(initiateAttack);
        if (initiateAttack < 1)
            canAttack = true;
        if (initiateAttack == 1)
            isAttacking = true;
        if (initiateAttack == 2)
            canThrow = true;
    }

    IEnumerator PrepareForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        canRun = true;
    }

    IEnumerator Throwing()
    {
        canThrow = false;
        yield return new WaitForSeconds(0.5f);
        Quaternion objectRotation = lancePosition.transform.rotation;
        Instantiate(lance, lancePosition.transform.position, objectRotation);
        canThrow = true;
    }

    IEnumerator DestroyLances()
    {
        yield return new WaitForSeconds(3f);
        canAttack = true;
        Destroy(lances[0]);
        Destroy(lances[1]);
        Destroy(lances[2]);
    }
}