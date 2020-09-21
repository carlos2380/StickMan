using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CtrlMenu : MonoBehaviour
{

    IEnumerator loadLevel(int numScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(numScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void goLevel(int numScene)
    {
        StartCoroutine(loadLevel(numScene));
    }

    public void goMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
