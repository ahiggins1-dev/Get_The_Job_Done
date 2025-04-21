/*****************************************************************************
// File Name : KeyCollectable.cs
// Author : Drew Higgins
// Creation Date : April 16th, 2025
//
// Brief Description : This script allows the player to collect the specific
                       collectable objects needed to progress.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyCollectable : MonoBehaviour, IInteractable
{
    [SerializeField] private TMP_Text collectText;
    [SerializeField] private TextAppear textAppear;

    private void Start()
    {
        collectText.enabled = false;
    }

    public void Interact()
    {
        Debug.Log("Key item found!");
        collectText.text = "You found a key!";
        collectText.enabled = true;
        textAppear.TextDisappear();
        Destroy(gameObject);

        //variable that allows the player to progress, will need a var in another script(?)
    }
}
