/*****************************************************************************
// File Name : QTEObject.cs
// Author : Drew Higgins
// Creation Date : March 28th, 2025
//
// Brief Description : This script allows the player to interact with objects
                       that start quick time events.
*****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

/// <summary>
/// References the interactable interface
/// </summary>
public class QTEObject : MonoBehaviour, IInteractable
{
    private int buttonGen;  //will use in later iterations of QTEs
    private int correctKey; //will use in later iterations of QTEs
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text completeText;
    [SerializeField] private TextAppear textAppear;
    [SerializeField] private int amountLeft;
    [SerializeField] private Animator animator;
    private bool isTimer;
    private Coroutine timerCo;
    public int increment;

    /// <summary>
    /// Disables the on-screen text for QTEs to not be active when the player starts the game
    /// </summary>
    private void Start()
    {
        buttonText.enabled = false;
        countdownText.enabled = false;

        PublicEvents.qtePressed += ButtonPressed;
    }
    
    /// <summary>
    /// Interact function from the interface, activates when the player interacts
    /// </summary>
    public void Interact()
    {
        //Enables the text explaining the QTE and showing the time limit
        buttonText.enabled = true;
        countdownText.enabled = true;

        Debug.Log("QTE Initiated!");

        //Checks if the object has the SpamQTE tag, will be updated with other QTE types in the future
        if(GameObject.FindGameObjectWithTag("SpamQTE"))
        {
            //enables the specific function
            SpamSequence();
        }
    }

    /// <summary>
    /// Creates the function for the internal timer if there is not a QTE already running
    /// </summary>
    private void SpamSequence()
    {
        if(!isTimer)
        {
            timerCo = StartCoroutine(Countdown());
        }
    }

    /// <summary>
    /// Creates the internal countdown that dictates how long the QTE lasts
    /// </summary>
    /// <returns></returns>
    private IEnumerator Countdown()
    {
        //References the PublicEvents script
        PublicEvents.qteStarted();

        //The timer is now active, and the number of times the player has performed the action is now 0
        isTimer = true;
        increment = 0;

        //This runs while there is still time left in the QTE
        while(amountLeft > 0)
        {
            //The countdown text updates every second
            countdownText.text = amountLeft.ToString();

            //Counts the timer down and updates the internal variable for seconds left
            yield return new WaitForSecondsRealtime(1);
            amountLeft--;
        }

        //After the QTE is done, turns the timer off and disables the text
        isTimer = false;
        PublicEvents.qteStopped();
        Debug.Log(increment);
        buttonText.enabled = false;
        countdownText.enabled = false;

        //The gameobject also disappears
        gameObject.SetActive(false);

        //Enables the text informing the player that the QTE has been completed
        completeText.enabled = true;

        //Tells the player how many times they hit Space
        completeText.text = "You hit the cube " + increment + " times! Wow!";

        //Runs the function that will make the text disappear after a few seconds
        textAppear.TextDisappear();
    }

    /// <summary>
    /// For if the correct button is pressed (Space)
    /// </summary>
    private void ButtonPressed()
    {
        //Plays the animation on the cube
        animator.CrossFadeInFixedTime("shakeanimation", 0);

        //If there is a timer, the increment will go up when the player hits the key
        if (isTimer)
        {
            increment++;
        }
    }

    /// <summary>
    /// When the object is disabled, the QTE ends and the animator of the gameobject is disabled
    /// </summary>
    private void OnDisable()
    {
        PublicEvents.qtePressed -= ButtonPressed;
        animator.enabled = false;
    }
}
