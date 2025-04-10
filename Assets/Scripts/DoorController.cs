/*****************************************************************************
// File Name : DoorController.cs
// Author : Drew Higgins
// Creation Date : March 31st, 2025
//
// Brief Description : This script creates the necessary functions to allow
                       the player to open the door.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// References the interactable interface
/// </summary>
public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator animator;
    
    /// <summary>
    /// The interact function enables the animation of the door opening
    /// </summary>
    public void Interact()
    {
        animator.enabled = true;

        Debug.Log("Door Opened!");
         
    }
}
