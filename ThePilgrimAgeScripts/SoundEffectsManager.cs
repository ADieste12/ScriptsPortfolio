using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;

    [SerializeField] AudioSource sfxObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }     
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //SPAWN EN GAMEOBJECT
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

        //ASIGNAR AUDIOCLIP 
        audioSource.clip = audioClip;

        //VOLUMEN
        audioSource.volume = volume;

        //PLAY
        audioSource.Play();

        //OBTENER LONGITUD DE CLIP
        float clipLength = audioSource.clip.length;

        //DESTRUIR CUANDO ACABE EL CLIP
        Destroy(audioSource.gameObject, clipLength);
    }
}
