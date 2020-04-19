using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string levelName = "Level0";
    public void Play()
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
