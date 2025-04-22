/*****************************************************************************
// File Name : TextAppear.cs
// Author : Drew Higgins
// Creation Date : March 30th, 2025
//
// Brief Description : This script allows on-screen text to disappear after
                       a set amount of time.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAppear : MonoBehaviour
{
    //private static TextAppear instance;              for future iterations
    [SerializeField] private TMP_Text collectText;

    /// <summary>
    /// This function is referenced in other scripts to help the text disappear
    /// </summary>
    public void TextDisappear()
    {
        StartCoroutine(TextDisable());
    }

    /// <summary>
    /// Creates the timer that sets how long the text stays on screen for before disappearing
    /// </summary>
    /// <returns></returns>
    IEnumerator TextDisable()
    {
        //instance = this;         for future iterations

        yield return new WaitForSeconds(3f);

        Debug.Log("Countdown Finished!");

        collectText.enabled = false;

        //instance = null;        for future iterations
    }
}
