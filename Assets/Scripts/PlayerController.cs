/*****************************************************************************
// File Name : PlayerController.cs
// Author : Drew Higgins
// Creation Date : March 20th, 2025
//
// Brief Description : This script creates the necessary functions for the 
                       player to function.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// An interface to create a function that can be referenced in multiple scripts
/// </summary>
interface IInteractable
{
    public void Interact();
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rampSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera playerCam;

    public int overallScore;
    [SerializeField] private EndGame endGame;

    private InputAction move;
    private InputAction moveCam;
    private InputAction interact;
    private InputAction restart;
    private InputAction quit;
    private InputAction spam;
    private InputAction keyOne;
    private InputAction keyTwo;
    private InputAction keyThree;

    private Rigidbody rb;
    private bool isMoving;
    private bool eventStatus;
    private Vector3 playerMovement;
    private Vector2 inputMovementValue;

    [SerializeField] private Transform interactSource;
    [SerializeField] private float interactRange;

    /// <summary>
    /// Start sets up all the necessary actions for the player
    /// </summary>
    void Start()
    {
        //Makes the mouse invisible and locks it to the middle of the screen
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        //Sets the QTE score of the stage to 0;
        overallScore = 0;

        //Accesses the action map and player body
        rb = GetComponent<Rigidbody>();
        playerInput.currentActionMap.Enable();

        //Finds the inputs
        move = playerInput.currentActionMap.FindAction("Move");
        moveCam = playerInput.currentActionMap.FindAction("Camera");
        interact = playerInput.currentActionMap.FindAction("Interact");
        restart = playerInput.currentActionMap.FindAction("Restart");
        quit = playerInput.currentActionMap.FindAction("Quit");
        spam = playerInput.currentActionMap.FindAction("Spam");
        keyOne = playerInput.currentActionMap.FindAction("RandomKey1");
        keyTwo = playerInput.currentActionMap.FindAction("RandomKey2");
        keyThree = playerInput.currentActionMap.FindAction("RandomKey3");

        //Creates the functions that will store code for these inputs
        move.started += Move_started;
        move.canceled += Move_canceled;
        interact.started += Interact_started;
        restart.started += Restart_started;
        quit.started += Quit_started;
        spam.started += Spam_started;
        keyOne.started += KeyOne_started;
        keyTwo.started += KeyTwo_started;
        keyThree.started += KeyThree_started;

        //Links to the class that will be used for QTE systems
        PublicEvents.qteStarted += QTEStarted;
        PublicEvents.qteStopped += QTESTopped;
    }

    private void KeyThree_started(InputAction.CallbackContext obj)
    {
        if (eventStatus)
        {
            PublicEvents.randomKeyPressed(RandomQTEKey.Key3);
        }
    }

    private void KeyTwo_started(InputAction.CallbackContext obj)
    {
        if (eventStatus)
        {
            PublicEvents.randomKeyPressed(RandomQTEKey.Key2);
        }
    }

    private void KeyOne_started(InputAction.CallbackContext obj)
    {
        if(eventStatus)
        {
            PublicEvents.randomKeyPressed(RandomQTEKey.Key1);
        }
    }

    /// <summary>
    /// Handles what happens if the player object is destroyed
    /// </summary>
    private void OnDestroy()
    {
        //Destroys all the functions so the player cannot access them anymore
        move.started -= Move_started;
        move.canceled -= Move_canceled;
        interact.started -= Interact_started;
        restart.started -= Restart_started;
        quit.started -= Quit_started;
        spam.started -= Spam_started;
        keyOne.started -= KeyOne_started;
        keyTwo.started -= KeyTwo_started;
        keyThree.started -= KeyThree_started;

        //Stops any active QTEs and makes it so the player cannot start any new ones
        PublicEvents.qteStarted -= QTEStarted;
        PublicEvents.qteStopped -= QTESTopped;
    }

    /// <summary>
    /// Controls what happens when a QTE is started
    /// </summary>
    private void QTEStarted()
    {
        //Tells the script that the QTE is happening
        eventStatus = true;
    }

    /// <summary>
    /// Controls what happens when a QTE is ended
    /// </summary>
    private void QTESTopped()
    {
        //Tells the script that the QTE has ceased
        eventStatus = false;
    }

    /// <summary>
    /// Allows the player to press Space in the QTEs
    /// </summary>
    /// <param name="obj"></param>
    private void Spam_started(InputAction.CallbackContext obj)
    {
        //If a QTE is happening, it allows the player to perform this input
        if (eventStatus)
        {
            PublicEvents.qtePressed();
            overallScore++;
            //endGame.finalScore += overallScore;  TEMPORARY
        }
    }

    /// <summary>
    /// Allows the player to quit the game
    /// </summary>
    /// <param name="obj"></param>
    private void Quit_started(InputAction.CallbackContext obj)
    {
        //Quits both the application and the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;

        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Allows the player to restart the scene
    /// </summary>
    /// <param name="obj"></param>
    private void Restart_started(InputAction.CallbackContext obj)
    {
        //Reloads the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Controls how the player can press E to interact with objects
    /// </summary>
    /// <param name="obj"></param>
    private void Interact_started(InputAction.CallbackContext obj)
    {
        //Creates the ray that goes out of the player body
        Ray r = new Ray(interactSource.position, playerCam.transform.forward);

        //Checks if the object is in range of the ray
        if(Physics.Raycast(r, out RaycastHit hitinfo, interactRange))
        {
            //Checks if the object is interactable
            if(hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                //Performs the Interact function in the interface
                interactObj.Interact();
            }
        }
    }

    /// <summary>
    /// What happens if the player stops pressing a movement key
    /// </summary>
    /// <param name="obj"></param>
    private void Move_canceled(InputAction.CallbackContext obj)
    {
        //Stops the player if they're not pressing the buttons
        isMoving = false;
        playerMovement = Vector3.zero;
    }

    /// <summary>
    /// Creates the variables that track what the player is pressing for movement
    /// </summary>
    /// <param name="obj"></param>
    private void Move_started(InputAction.CallbackContext obj)
    {
        //Creates the variables allowing the player to move
        isMoving = true;
        inputMovementValue = obj.ReadValue<Vector2>();
    }

    /// <summary>
    /// What allows the first-person camera to function
    /// </summary>
    /// <param name="iValue"></param>
    private void OnCamera(InputValue iValue)
    {
        //Gets the value of the camera
        Vector2 cameraMovementValue = iValue.Get<Vector2>();
        FindObjectOfType<CameraMovement>().PlayerLook(cameraMovementValue);
    }

    /// <summary>
    /// Controls movement when players are outside of a QTE
    /// </summary>
    void Update()
    {
        if (!eventStatus)
        {
            //Checks if the player is doing the movement input
            if (isMoving == true)
            {
                //Reads the value of the movement
                inputMovementValue = move.ReadValue<Vector2>();

                //Defines how the player moves by allowing all forms of movement (right, left, forward, etc.)
                playerMovement = orientation.forward * (playerSpeed * rampSpeed) * inputMovementValue.y + 
                    orientation.right * (playerSpeed * rampSpeed) * inputMovementValue.x;
            }

            //Moves the rigidbody itself
            rb.velocity = new Vector3(playerMovement.x, 0, playerMovement.z);
        }
    }
}
