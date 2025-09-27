using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Glove : MonoBehaviour
{
    [SerializeField] Collider2D attackCollider;
    bool isAttackPressed;
    SpriteRenderer sp;
    [SerializeField] Controls playerInput;

    void Start()
    {
        playerInput = new Controls();
    }

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnEnable()
    {
        attackCollider.enabled = false;
    }

    private void OnDisable()
    {
        attackCollider.enabled = false;
    }

    public void AttackGuante()
    {
        attackCollider.enabled = true;
        StartCoroutine (Collider());       
    }    

    IEnumerator Collider()
    {
        yield return new WaitForSeconds(0.6f);
        attackCollider.enabled = false;
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        attackCollider.enabled = false;
    }
}
