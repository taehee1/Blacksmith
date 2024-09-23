using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public void Exit()
    {
        Application.Quit();
    }

    public void InGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Battle()
    {
        SceneManager.LoadScene("Battle");
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
