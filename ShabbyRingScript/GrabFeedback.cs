using UnityEngine;

public class GrabFeedback : MonoBehaviour
{
    Collider2D feedback;
    SpriteRenderer sp;
    public BasicMove bM;
    void Start()
    {
        feedback = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (bM.enabled == false)
        {
            feedback.enabled = true;
            sp.enabled = true;
        }
        else
        {
            feedback.enabled = false;
            sp.enabled = false;
        }
    }
}
