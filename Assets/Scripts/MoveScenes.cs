/*****************************************************************************
// File Name : MoveScenes.cs
// Author : Drew Higgins
// Creation Date : March 31st, 2025
//
// Brief Description : This script is what allows the player to move from
                       one level to the next by interacting with a special
                       door.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// References the interface
/// </summary>
public class MoveScenes : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Uses the global interact function to move the scene forward by one
    /// </summary>
   public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
