using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitcher : MonoBehaviour
{
    public static void switchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void quitGame()
    {
        Application.Quit();
    }

    
}
