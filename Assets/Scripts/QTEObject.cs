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
using Unity.VisualScripting;

//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public enum RandomQTEKey
{
    Key1, Key2, Key3
}

/// <summary>
/// References the interactable interface
/// </summary>
public class QTEObject : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject keyObjective;
    [SerializeField] private bool keySpawner;
    [SerializeField] private int objectiveCall;

    private int buttonGen;  //will use in later iterations of QTEs
    private RandomQTEKey correctKey; 
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text completeText;
    [SerializeField] private TMP_Text objectiveText;

    [SerializeField] private TextAppear textAppear;
    [SerializeField] private int amountLeft;
    [SerializeField] private Animator animator;
    private bool isTimer;
    private bool decideQTE;
    private Coroutine timerCo;
    public int increment;

    /// <summary>
    /// Disables the on-screen text for QTEs to not be active when the player starts the game
    /// </summary>
    private void Start()
    {
        buttonText.enabled = false;
        countdownText.enabled = false;
        animator = GetComponent<Animator>();

        PublicEvents.qtePressed += ButtonPressed;
        PublicEvents.randomKeyPressed += RandomButtonPressed;
    }
    
    /// <summary>
    /// Interact function from the interface, activates when the player interacts
    /// </summary>
    public void Interact()
    {
        //Enables the text explaining the QTE and showing the time limit
        //buttonText.enabled = true;
        //countdownText.enabled = true;

        Debug.Log("QTE Initiated!");

        //Checks if the object has the SpamQTE tag, will be updated with other QTE types in the future
        if(gameObject.CompareTag("SpamQTE"))
        {
            buttonText.text = "Press Space!";
            buttonText.enabled = true;
            countdownText.enabled = true;
            decideQTE = false;
            SpamSequence();
        }

        else if(gameObject.CompareTag("RandomQTE"))
        {
            buttonText.text = "Press Z!";
            buttonText.enabled = true;
            countdownText.enabled = true;
            decideQTE = true;
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
        gameObject.SetActive(true);
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

        //Enables any key objects needed to spawn by completing the QTE
        if(keySpawner == true)
        {
            keyObjective.gameObject.SetActive(true);
        }

        if(objectiveCall != 0)
        {
            objectiveText.text = "Objective: " + ObjectiveList.Objectives[objectiveCall];
        }
       

        //Enables the text informing the player that the QTE has been completed
        completeText.enabled = true;

        //Tells the player how many times they hit Space
        completeText.text = "You completed " + increment + " actions! Wow!";

        //Runs the function that will make the text disappear after a few seconds
        textAppear.TextDisappear();
    }

    /// <summary>
    /// For if the correct button is pressed (Space)
    /// </summary>
    private void ButtonPressed()
    {
        if (decideQTE == false)
        {
            //Plays the animation on the cube
            animator.CrossFadeInFixedTime("shakeanimation", 0);

            //If there is a timer, the increment will go up when the player hits the key
            if (isTimer)
            {
                increment++;
            }
        }
    }

    private void RandomButtonPressed(RandomQTEKey keyPressed)
    {
        if (decideQTE == true)
        {
            if (keyPressed == correctKey)
            {
                //Plays the animation on the cube
                Debug.Log(animator.name);
                animator.CrossFadeInFixedTime("flipanimation", 0);

                //If there is a timer, the increment will go up when the player hits the key
                if (isTimer)
                {
                    increment++;
                }

                correctKey = (RandomQTEKey)UnityEngine.Random.Range(0, 3);
                switch (correctKey)
                {
                    case RandomQTEKey.Key1:
                        buttonText.text = "Press Z!";
                        break;
                    case RandomQTEKey.Key2:
                        buttonText.text = "Press G!";
                        break;
                    case RandomQTEKey.Key3:
                        buttonText.text = "Press N!";
                        break;
                    default:
                        Debug.Log("Error! Error!");
                        break;
                }
            }
        }
    }


    private void OnEnable()
    {
        //animator = GetComponent<Animator>();
    }

    /// <summary>
    /// When the object is disabled, the QTE ends and the animator of the gameobject is disabled
    /// </summary>
    private void OnDisable()
    {
        PublicEvents.qtePressed -= ButtonPressed;
    }
}
