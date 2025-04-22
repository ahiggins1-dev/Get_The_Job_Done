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
    public static bool hasKey;
    [SerializeField] private TMP_Text collectText;
    [SerializeField] private TextAppear textAppear;
    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private int objectiveCall;

    private void Start()
    {
        hasKey = false;
        collectText.enabled = false;

        if(gameObject.CompareTag("ObjectiveKey"))
        {
            gameObject.SetActive(false);
            Debug.Log("Objective keys hidden!");
        }

        
    }

    public void Interact()
    {
        hasKey = true;
        Debug.Log("Key item found!");
        collectText.text = "You found a key!";
        collectText.enabled = true;
        textAppear.TextDisappear();
        Destroy(gameObject);

        if(gameObject.CompareTag("ObjectiveKey"))
        {
            objectiveText.text = "Objective: " + ObjectiveList.Objectives[objectiveCall];
        }
    }
}
