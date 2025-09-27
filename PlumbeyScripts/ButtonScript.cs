using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject flecha;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        flecha.SetActive(true);
        audioManager.PlaySFX(audioManager.botonHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        flecha.SetActive(false);
        audioManager.PlaySFX(audioManager.botonHover);
    }
}
