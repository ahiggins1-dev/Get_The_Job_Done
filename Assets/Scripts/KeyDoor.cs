/*****************************************************************************
// File Name : KeyDoor.cs
// Author : Drew Higgins
// Creation Date : April 21st, 2025
//
// Brief Description : This script allows the player to interact with specific
                       doors once they have keys
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private TMP_Text collectText;
    [SerializeField] private TextAppear textAppear;
    [SerializeField] private Animator animator;

    /// <summary>
    /// This function will detect if a player has a key and respond accordingly
    /// </summary>
    public void Interact()
    {
        //If the player has a key, the door will open
        if (KeyCollectable.hasKey == true)
        {
            Debug.Log("Key door opened.");
            collectText.text = "Door unlocked!";
            collectText.enabled = true;
            textAppear.TextDisappear();

            animator.enabled = true;

            KeyCollectable.hasKey = false;
        }
        //If not, the text telling them what they need will appear
        else
        {
            collectText.text = "You need a key.";
            collectText.enabled = true;
            textAppear.TextDisappear();
        }
    }
}
