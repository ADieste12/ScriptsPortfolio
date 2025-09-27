using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SaveController : MonoBehaviour
{
    public GameObject player;
    public GameObject bunnyBoss;
    public GameObject bossDoor;
    public GameObject bossDoor1;
    GameObject finalBoss;
    public static string archivoGuardado;
    public Save save = new Save();
    public DayNight dN;
    public PlayerLife pL;
    bool bunnyIsDead;
    int saveLimit;
    public GameObject key1;
    public GameObject key2;

    void Awake()
    {
        archivoGuardado = Application.dataPath + "/save.json";

        player = GameObject.FindGameObjectWithTag("Player");
        bunnyBoss = GameObject.Find("BunnyBoss");
        finalBoss = GameObject.Find("FinalBoss");
        bossDoor = GameObject.Find("BossDoor");
        bossDoor1 = GameObject.Find("BossDoor1");
        LoadFile();
    }

    private void Update()
    {
        if (pL.life <= 0)
        {
            LoadFile();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (dN.isInside)
        {
            saveLimit++;
            if (saveLimit == 1)
            {
                SaveFile();
                Debug.Log("Saved");
            }
        }

        if (!dN.isInside)
        {
            saveLimit = 0;
        }

        if (bunnyBoss == null)
        {
            bunnyIsDead = true;
        }
        //SI MATAS AL BOSS FINAL Y TE ACABAS EL JUEGO EL ARCHIVO SE RESETEA Y VUELVES AL INICIO SI REINICIAS EL JUEGO
        if (finalBoss == null)
        {
            DeleteFile();
        }
    }

    private void LoadFile()
    {
        //SI EXISTE UN ARCHIVO DE GUARDADO ESTE METODO INSTANCIA TODAS LAS VARIABLES GUARDADAS EN LA ESCENA RECIEN CARGADA
        if (File.Exists(archivoGuardado))
        {
            string contenido = File.ReadAllText(archivoGuardado);
            save = JsonUtility.FromJson<Save>(contenido);
            player.transform.position = save.position;
            bunnyIsDead = save.stateOfBunny;
            player.GetComponent<BasicMove>().keyCount = save.key;
            if (bunnyIsDead)
            {
                Destroy(bunnyBoss);
                Destroy(bossDoor);
                Destroy(bossDoor1);
            }
            key1.SetActive(save.key1Active);
            key2.SetActive(save.key2Active);
        }
    }
    void SaveFile()
    {
        //GUARDA UN ARCHIVO SOBREESCRIBIENDO EL ANTERIOR
        Save newFile = new Save()
        {
            position = player.transform.position,
            stateOfBunny = bunnyIsDead,
            key = player.GetComponent<BasicMove>().keyCount,
            key1Active = key1.activeInHierarchy,
            key2Active = key2.activeInHierarchy
        };

        string cadenaJSON = JsonUtility.ToJson(newFile);
        
        File.WriteAllText(archivoGuardado, cadenaJSON);
    }
    static void DeleteFile()
    {
        //RESETEA TODO LO GUARDADO Y CREA UN NUEVO ARCHIVO DE 0
        Save newFile = new Save()
        {
            position = new Vector3(18.26f, 1.84f, 0f),
            stateOfBunny = false,
            key = 0,
            key1Active = false,
            key2Active = false
        };

        string cadenaJSON = JsonUtility.ToJson(newFile);

        File.WriteAllText(archivoGuardado, cadenaJSON);
    }
}
