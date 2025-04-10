/*****************************************************************************
// File Name : EndGame.cs
// Author : Drew Higgins
// Creation Date : March 31st, 2025
//
// Brief Description : This script lets the player end the game via a button
                       on the last screen of the game.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private TMP_Text endScoreText;

    [SerializeField] public int finalScore;
    private QTEObject qteObject;

    /// <summary>
    /// Unlocks the player cursor and makes it visible again
    /// </summary>
    private void Start()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        if (finalScore >= 60)
        {
            endScoreText.text = "You scored a " + finalScore + "! Congratulations!";
        }
        else
            endScoreText.text = "You scored a " + finalScore + ". Aim for a higher score next time!";
    }

    /// <summary>
    /// Script that is on the button to allow the game to end
    /// </summary>
    private void EndExperience()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }
}
