using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public static SplashScreen instance {get; private set;}

    public string nameOfMainMenuScene;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple SplashScrene Scripts found in this scene.  There should only be one.");
        }
        instance = this;
    }

    public void OnStartGame()
    {
        SceneManager.LoadSceneAsync(nameOfMainMenuScene);
    }
}
