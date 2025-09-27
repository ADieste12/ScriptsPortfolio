using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntermissionTime : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("Intermission");
    }

    IEnumerator Intermission()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(2);
    }

}
