/*****************************************************************************
// File Name : CameraMovement.cs
// Author : Drew Higgins
// Creation Date : March 25th, 2025
//
// Brief Description : This script allows the player to control the
                       first-person camera.
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float ySens;
    [SerializeField] private float xSens;
    [SerializeField] private Transform orientation;

    private bool eventStatus;
    private float range;
    private UnityEngine.Vector2 rotationValue;

    /// <summary>
    /// Creates the functions that are related to publicevents
    /// </summary>
    private void Start()
    {
        PublicEvents.qteStarted += QTEStarted;

        PublicEvents.qteStopped += QTEStopped;
    }

    /// <summary>
    /// Disables the functions
    /// </summary>
    private void OnDestroy()
    {
        PublicEvents.qteStarted -= QTEStarted;

        PublicEvents.qteStopped -= QTEStopped;
    }

    /// <summary>
    /// Enables the eventstatus
    /// </summary>
    private void QTEStarted()
    {
        eventStatus = true;
    }

    /// <summary>
    /// Disables the eventstatus
    /// </summary>
    private void QTEStopped()
    {
        eventStatus = false;
    }

    /// <summary>
    /// The code that allows the player to look around by checking the mouse position
    /// </summary>
    /// <param name="position"></param>
    public void PlayerLook(UnityEngine.Vector2 position)
    {
        //Can only move the camera when the player is not in a QTE
        if (!eventStatus)
        {
            float tempX;
            float tempY;

            tempX = position.x * Time.deltaTime * xSens;
            tempY = position.y * Time.deltaTime * ySens;

            rotationValue.x -= tempY;
            rotationValue.y += tempX;

            //Adds a clamp so the player can only look 90 degrees up and down
            rotationValue.x = Mathf.Clamp(rotationValue.x, -90, 90);

            transform.rotation = UnityEngine.Quaternion.Euler(rotationValue.x, rotationValue.y, 0);

            orientation.rotation = UnityEngine.Quaternion.Euler(0, rotationValue.y, 0);
        }
    }
}
