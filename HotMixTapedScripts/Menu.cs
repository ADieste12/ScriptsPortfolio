using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    private Button button;
    public GameManager gameManager;
    public int empezar;
    public int config;
    public int levels;
    public int configClose;
    public int exit;
    public int cont;

    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        
    }
    void Jugar()
    {
        gameManager.StartGame(empezar);
    }

    void Configuracion()
    {
        gameManager.Options(config);
    }

    void ConfiguracionCerrar()
    {
        gameManager.CloseOptions(configClose);
    }

    void Selector()
    {
        gameManager.Select(levels);
    }

    void Salir()
    {
        gameManager.ExitGame(exit);
    }
}
