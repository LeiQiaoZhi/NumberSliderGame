using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadScene()
    {
        XLogger.Log(Category.Scene,"reload scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(int _index)
    {
        XLogger.Log(Category.Scene,$"Loading scene {SceneManager.GetSceneByBuildIndex(_index).name}");
        SceneManager.LoadScene(_index, LoadSceneMode.Single);
    }

    public void LoadSceneWithLoadingScreen(int _index)
    {
        // TODO
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
