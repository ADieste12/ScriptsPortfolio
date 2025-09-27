using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class GrabObjects : MonoBehaviour
{
    public Transform grabDetecter;
    public Transform objectHolder;
    public GameObject personajeAgarrado;
    public float rayDist;
    public bool isPressed;
    [SerializeField] Controls playerInput;
    public List <Collider2D> grabList; 
    public int grabCheck;
    
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] float radioObject;

    public ContactFilter2D contactFilter;
    
    [SerializeField] GameObject arms;
    public GameObject[] listaBrazos; //Referenciar los gameobjects y las tags en el mismo orden
    public string[] listaTags;

    public GameObject itemAgarrado;

    public SpriteRenderer sp;
    public bool isGrabbing = false;
    public Collider2D colObjectHolder;
    Collider2D colPlayer;
    public ObjectFeedback oF;
    public bool playerGrabbing = false;

    private void Awake()
    {
        playerInput = new Controls();
    }

    private void Start()
    {
        colPlayer = GetComponent<Collider2D>();
        contactFilter.SetLayerMask(objectLayer);
    }

    void Update()
    {
        //Si se rompe el item volvemos a activar los brazos
        if (itemAgarrado != null && itemAgarrado.GetComponent<Items>().destrozado == true)
        {
            ResetearBrazos();
        }
    }

    public void ResetearBrazos()
    {
        for (int i = 0; i < listaBrazos.Length; i++)
        {
            listaBrazos[i].SetActive(false);
        }
        arms.SetActive(true);

        if (itemAgarrado != null)
        {
            print("soltar item");
            itemAgarrado.gameObject.transform.parent = null;
            itemAgarrado.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            itemAgarrado = null;
        }

        sp.enabled = true;
        sp = null;

        isGrabbing = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(grabDetecter.position, radioObject);
    }

    IEnumerator DeactivateCollider()
    {
        yield return new WaitForSeconds(0.5f);
        colObjectHolder.enabled = false;
    }

    IEnumerator SoltarPersonaje()
    {
        yield return new WaitForSeconds(5);
        playerGrabbing = false;
        if (personajeAgarrado != null)
        {
            personajeAgarrado.transform.parent = null;
            personajeAgarrado.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            personajeAgarrado.GetComponent<BasicMove>().enabled = true;
            personajeAgarrado.GetComponent<GrabObjects>().enabled = true;
            personajeAgarrado.GetComponent<ObjectFeedback>().enabled = true;
            personajeAgarrado.transform.eulerAngles = new Vector2(0f, 0f);
            isGrabbing = false;
            if (sp != null) sp.enabled = true;
            sp = null;
            personajeAgarrado = null;
            colObjectHolder.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZonaMuerte"))
        {
            if (personajeAgarrado != null)
            {
                print("mesuelto");
                playerGrabbing = false;
                personajeAgarrado.transform.parent = null;
                personajeAgarrado.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                personajeAgarrado.GetComponent<BasicMove>().enabled = true;
                personajeAgarrado.GetComponent<GrabObjects>().enabled = true;
                personajeAgarrado.GetComponent<ObjectFeedback>().enabled = true;
                personajeAgarrado.GetComponent<SpriteRenderer>().enabled = true;
                personajeAgarrado.transform.eulerAngles = new Vector2(0f, 0f);
                personajeAgarrado = null;
            }
            isGrabbing = false;

            colObjectHolder.enabled = false;
        }
    }

    void OnInteract(InputValue value)
    {
        isPressed = value.isPressed;
        if (isPressed)
        {
            //Si hay un personaje agarrado lo suelta y sale de la funcion
            if (personajeAgarrado != null)
            {
                print("soltar personaje");
                personajeAgarrado.transform.parent = null;
                personajeAgarrado.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                personajeAgarrado.GetComponent<BasicMove>().enabled = true;
                personajeAgarrado.GetComponent<GrabObjects>().enabled = true;
                personajeAgarrado.GetComponent<ObjectFeedback>().enabled = true;
                personajeAgarrado.transform.eulerAngles = new Vector2(0f, 0f);
                isGrabbing = false;
                sp.enabled = true;
                sp = null;
                personajeAgarrado = null;
                colObjectHolder.enabled = true;
                playerGrabbing = false;
                StartCoroutine(DeactivateCollider());
                return;
            }

            //Detecta todos los colliders con los que este en contacto y los guarda en una lista
            grabCheck = Physics2D.OverlapCircle(grabDetecter.position, radioObject, contactFilter, results: grabList);

            //Eliminamos de la lista el propio collider y el item que tiene agarrado si lo tiene
            grabList.Remove(colPlayer);
            if (itemAgarrado != null) {
                grabList.Remove(itemAgarrado.GetComponent<Collider2D>());
                ResetearBrazos();
            }

            //Recorremos la lista de todos los objetos agarrables
            for (int i = 0; i < grabList.Count; i++)
            {
                //Coger personaje
                if (grabList[i].CompareTag("pickPlayer"))
                {
                    if (itemAgarrado != null)
                    {
                        ResetearBrazos();
                    }

                    if (!isGrabbing)
                    {
                        print("coger personaje");
                        sp = grabList[i].GetComponent<SpriteRenderer>();
                        personajeAgarrado = grabList[i].gameObject;
                        personajeAgarrado.transform.parent = objectHolder;
                        personajeAgarrado.transform.position = objectHolder.position;
                        personajeAgarrado.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                        personajeAgarrado.GetComponent<BasicMove>().enabled = false;
                        personajeAgarrado.GetComponent<GrabObjects>().enabled = false;
                        personajeAgarrado.GetComponent<ObjectFeedback>().enabled = false;
                        isGrabbing = true;
                        sp.enabled = false;
                        StartCoroutine(SoltarPersonaje());
                        playerGrabbing = true;
                    }
                    grabList.Clear();
                    return;
                }

                //Coger objeto
                if ((grabList[i].gameObject.layer == LayerMask.NameToLayer("Object")))
                {
                    if (!isGrabbing)
                    {
                        //Guardo una referencia del item agarrado
                        print("coger item");
                        itemAgarrado = grabList[i].gameObject;
                        sp = grabList[i].GetComponent<SpriteRenderer>();

                        itemAgarrado.gameObject.transform.parent = objectHolder;
                        itemAgarrado.gameObject.transform.position = objectHolder.position;
                        itemAgarrado.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                        sp.enabled = false;
                        arms.SetActive(false);

                        for (int j = 0; j < listaTags.Length; j++)
                        {
                            if (grabList[i].tag == listaTags[j])
                            {
                                listaBrazos[j].SetActive(true);
                                break;
                            }
                        }
                        isGrabbing = true;
                    }
                }
            }
        }
    }
}