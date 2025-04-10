/*****************************************************************************
// File Name : CollectableController.cs
// Author : Drew Higgins
// Creation Date : March 25th, 2025
//
// Brief Description : This script allows the player to collect the specific
                       collectable objects scattered in the levels
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// References the interactable interface
/// </summary>
public class CollectableController : MonoBehaviour, IInteractable
{
    [SerializeField] private TMP_Text collectText;
    [SerializeField] private TextAppear textAppear;

    /// <summary>
    /// Disables the on-screen text for the collection on the game start
    /// </summary>
    private void Start()
    {
        collectText.enabled = false;
    }

    /// <summary>
    /// Enables the collection text and runs the disappear function for the text before destroying the gameobject 
    /// </summary>
    public void Interact()
    {
        Debug.Log("Collectable Found!");
        collectText.text = "You got a collectable! Nice job!";
        collectText.enabled = true;
        textAppear.TextDisappear();
        Destroy(gameObject);
    }
}
