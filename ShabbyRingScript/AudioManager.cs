using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("-----Audio Clip-----")]
    public AudioClip salto;
    public AudioClip caer;
    public AudioClip codazo;
    public AudioClip objetoAgarrar;
    public AudioClip objetoFallo;
    public AudioClip maletin;
    public AudioClip puño;
    public AudioClip silla;
    public AudioClip trampa;

    public void PlaySFX (AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
