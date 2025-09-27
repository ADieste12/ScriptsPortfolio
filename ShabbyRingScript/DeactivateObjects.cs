using System.Collections;
using UnityEngine;

public class DeactivateObjects : MonoBehaviour
{
    public GrabObjects gO;
    public BasicMove bM;
    public GameObject arms;     
    public GameObject maletin;     
    public GameObject guantes;     
    public GameObject silla;     

    void Update()
    {
        if (gO.enabled == false)
        {
            maletin.SetActive(false);
            guantes.SetActive(false);
            arms.SetActive(false);
            silla.SetActive(false);
        }
        else if (gO.enabled == true && bM.enabled == true)
        {
            if (gO.isGrabbing == true && gO.itemAgarrado != null)
            {
                if (gO.itemAgarrado.tag == "GuanteItem")
                {
                    guantes.SetActive(true);
                    maletin.SetActive(false);
                    arms.SetActive(false);
                    silla.SetActive(false);
                }
                else if (gO.itemAgarrado.tag == "MaletinItem")
                {
                    maletin.SetActive(true);
                    arms.SetActive(false);
                    guantes.SetActive(false);
                    silla.SetActive(false);
                }
                else if (gO.itemAgarrado.tag == "SillaItem")
                {
                    maletin.SetActive(false);
                    arms.SetActive(false);
                    guantes.SetActive(false);
                    silla.SetActive(true);
                }
            }
            else
            {
                maletin.SetActive(false);
                guantes.SetActive(false);
                arms.SetActive(true);
                silla.SetActive(false);
            }
        }
        if (gO.enabled == false && bM.enabled == false && gO.itemAgarrado != null && gO.isGrabbing == true)
        {
            gO.itemAgarrado.GetComponent<Collider2D>().enabled = false;
            gO.isGrabbing = false;
            gO.itemAgarrado.transform.parent = null;
            Debug.Log("�am");
        }

    }

    IEnumerator ObjectCollider()
    {
        yield return new WaitForSeconds(0.2f);
        gO.itemAgarrado.gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
