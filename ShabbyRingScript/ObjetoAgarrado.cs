using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class ObjetoAgarrado : MonoBehaviour
{
    [SerializeField] Collider2D attackCollider;
    Animator anim;
    bool isAttackPressed;
    public float cooldown;
    public int daño;
    public float fuerzaEmpuje;
    SpriteRenderer sp;
    [SerializeField] Controls playerInput;
    BasicMove scripMove;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        attackCollider = GetComponent<Collider2D>();
        attackCollider.enabled = false;
        anim = GetComponent<Animator>();
        scripMove = GetComponentInParent<BasicMove>();
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        playerInput = new Controls();
    }

    private void OnEnable()
    {
        attackCollider.enabled = false;
        scripMove.objectItemBrazos = this.gameObject;
        scripMove.itemBrazosAnim = anim;
    }

    private void OnDisable()
    {
        attackCollider.enabled = false;
        scripMove.objectItemBrazos = null;
        scripMove.itemBrazosAnim = null;
    }

    public void Attack()
    {
        attackCollider.enabled = true;
        StartCoroutine(Collider());
    }

    IEnumerator Collider()
    {
        yield return new WaitForSeconds(cooldown);
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            //Hacemos el knockback
            collision.GetComponent<Knockback>().PlayFeedback(gameObject, fuerzaEmpuje);

            //Guardamos la referencia del objectfeedback
            ObjectFeedback of = collision.gameObject.GetComponent<ObjectFeedback>();
            if (!of.isHitted || !of.isStuned)
            {
                //Se hace daño para el stun
                of.RestarStun(daño);

                //Se resta durabilidad al item
                GameObject jugador = transform.parent.gameObject;
                Items item = jugador.gameObject.GetComponentInChildren<Items>();
                item.RestarDuravilidad();
            }
        }
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        attackCollider.enabled = false;
    }
}
