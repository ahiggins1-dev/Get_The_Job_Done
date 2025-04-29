/*****************************************************************************
// File Name : StartGame.cs
// Author : Drew Higgins
// Creation Date : March 31st, 2025
//
// Brief Description : This script allows the player to press a button to
                       start the game.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    /// <summary>
    /// Moves the player to the next scene, which effectively starts the game
    /// </summary>
    /// 

    private void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
    }
    private void StartExperience()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadTutorial()
    {
        SceneManager.LoadSceneAsync(5);
    }

    private void QuitExperience()
    {
        //UnityEditor.EditorApplication.isPlaying = false;

        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }
}
