using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
   

    [Header("-----Audio Clip-----")]
    public AudioClip botonHover;
    public AudioClip botonPlay;
    public AudioClip botonClick;
    public AudioClip cambioGravedad;
    public AudioClip cambioGravedad2;
    public AudioClip cambioGravedad3;
    public AudioClip cambioGravedad4;
    public AudioClip cambioGravedad5;
    public AudioClip ganar;
    public AudioClip perder;
    public AudioClip ventosa;
    public AudioClip ventosa2;
    public AudioClip ventosa3;
    public AudioClip musicaMenu;
    public AudioClip musicaNivel;


    public static AudioManager instance;


    //DESDE AQUI CONTROLO LOS DIFERENTES CLIPS DE AUDIO DEL JUEGO 
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //ES UNA FUNCION PUBLICA PARA PODER LLAMARLA EN OTROS SCROPTS CUANDO NECESITE SITUACIONES ESPECIFICAS PARA USAR LOS DIFERENTES SONIDOS
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    private void Start()
    {
        musicSource.clip = musicaMenu;
    
        musicSource.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            musicSource.clip = musicaMenu;

            musicSource.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            musicSource.clip = musicaNivel;

            musicSource.Play();
        }

    }
}