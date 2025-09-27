using UnityEngine;

public class DayNight : MonoBehaviour
{
    public bool isInside;
    public bool isDay; 
    public GameObject nightDoor;
    public GameObject dayDoor;
    public Material daynightPlayer;
    public Material daynightMap;
    public Material porton;
    public Material farolillo;
    public Material cofre;
    public Material estatua;
    public Material escombros;
    public Material background;

    void Update()
    {
        //CAMBIO DE DIA Y NOCHE
        if (isDay)
        {
            daynightPlayer.SetFloat("_Threshold", 0.93f);
            daynightMap.SetFloat("_Threshold", 0.5f);
            porton.SetFloat("_Threshold", 0.5f);
            farolillo.SetFloat("_Threshold", 0.5f);
            cofre.SetFloat("_Threshold", 0.5f);
            estatua.SetFloat("_Threshold", 0.5f);
            escombros.SetFloat("_Threshold", 0.5f);
            background.SetFloat("_Threshold", 0.2f);
            dayDoor.SetActive(false);
            nightDoor.SetActive(true);
        }
        else
        {
            daynightPlayer.SetFloat("_Threshold", 0);
            daynightMap.SetFloat("_Threshold", 0);
            porton.SetFloat("_Threshold", 0);
            farolillo.SetFloat("_Threshold", 0);
            cofre.SetFloat("_Threshold", 0);
            estatua.SetFloat("_Threshold", 0);
            escombros.SetFloat("_Threshold", 0);
            background.SetFloat("_Threshold", 0);
            dayDoor.SetActive(true);
            nightDoor.SetActive(false);
        }
    }
}
